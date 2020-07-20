using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sso.Server.Api.Model;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public class ExternalProviderAuth
    {
        private readonly IHttpContextAccessor _htc;
        private readonly IEventService _events;
        private readonly IIdentityServerInteractionService _interaction;
        public ExternalProviderAuth(IHttpContextAccessor htc, IEventService events, IIdentityServerInteractionService interaction)
        {
            this._htc = htc;
            this._events = events;
            this._interaction = interaction;
        }

        public async Task<IActionResult> Callback(string returnUrl, Func<string, Task<UserCredential>> getUser)
        {
            // read external identity from the temporary cookie
            var info = await this._htc.HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var tempUser = info?.Principal;
            if (tempUser == null)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var claims = tempUser.Claims.ToList();

            // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            // remove the user id claim from the claims collection and move to the userId property
            // also set the name of the external authentication provider
            claims.Remove(userIdClaim);
            var provider = info.Properties.Items["scheme"];
            var userId = userIdClaim.Value;

            var email = claims.Where(_ => _.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").SingleOrDefault().Value;
            var nome = claims.Where(_ => _.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").SingleOrDefault().Value;

            //External 
            var user = await getUser(email);

            AuthenticationProperties props = null;
            // issue authentication cookie for user
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.SubjectId, user.Username));
            await this._htc.HttpContext.SignInAsync(user.SubjectId, user.Username, provider, props, user.Claims.ToArray());

            // delete temporary cookie used during external authentication
            await this._htc.HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // validate return URL and redirect back to authorization endpoint or a local page
            if (_interaction.IsValidReturnUrl(returnUrl))
                return new RedirectResult(returnUrl);

            return new RedirectResult("~/");
        }

        public async Task<IActionResult> Login(IUrlHelper url, string provider, string returnUrl)
        {
            var externalLoginCallback = url.Action("ExternalLoginCallback", new
            {
                returnUrl
            });

            // windows authentication is modeled as external in the asp.net core authentication manager, so we need special handling
            if (AccountOptions.WindowsAuthenticationSchemeName.Contains(provider))
            {
                // but they don't support the redirect uri, so this URL is re-triggered when we call challenge
                if (this._htc.HttpContext.User is WindowsPrincipal wp)
                {
                    var props = new AuthenticationProperties();
                    props.Items.Add("scheme", AccountOptions.WindowsAuthenticationSchemeName);

                    var id = new ClaimsIdentity(provider);
                    id.AddClaim(new Claim(JwtClaimTypes.Subject, this._htc.HttpContext.User.Identity.Name));
                    id.AddClaim(new Claim(JwtClaimTypes.Name, this._htc.HttpContext.User.Identity.Name));

                    // add the groups as claims -- be careful if the number of groups is too large
                    if (AccountOptions.IncludeWindowsGroups)
                    {
                        var wi = wp.Identity as WindowsIdentity;
                        var groups = wi.Groups.Translate(typeof(NTAccount));
                        var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                        id.AddClaims(roles);
                    }

                    await this._htc.HttpContext.SignInAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme, new ClaimsPrincipal(id), props);
                    return new RedirectResult(externalLoginCallback);
                }
                else
                {
                    // this triggers all of the windows auth schemes we're supporting so the browser can use what it supports
                    return new ChallengeResult(AccountOptions.WindowsAuthenticationSchemeName);
                }
            }
            else
            {
                // start challenge and roundtrip the return URL
                var props = new AuthenticationProperties
                {
                    RedirectUri = externalLoginCallback,
                    Items = { { "scheme", provider } }
                };
                return new ChallengeResult(provider, props);
            }
        }

    }
}
