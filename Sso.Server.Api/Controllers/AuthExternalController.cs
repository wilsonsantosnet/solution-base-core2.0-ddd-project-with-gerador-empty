using Common.Domain.Base;
using IdentityModel.Client;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Sso.Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthExternalController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOptions<ConfigSettingsBase> _configSettingsBase;
        private readonly IEventService _events;
        private readonly IUserCredentialServices _userServices;

        public AuthExternalController(ILoggerFactory logger, IOptions<ConfigSettingsBase> configSettingsBase, IEventService events, IUserCredentialServices userServices)
        {
            this._logger = logger.CreateLogger<AuthController>();
            this._configSettingsBase = configSettingsBase;
            this._logger.LogInformation("AccountController init success");
            this._events = events;
            this._userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormCollection accountCredential)
        {
            var appClientId = Uri.EscapeDataString(accountCredential["ClientId"]);
            var appClientSecret = Uri.EscapeDataString(accountCredential["ClientSecret"]);

            var user = Uri.EscapeDataString(accountCredential["User"]);
            var password = Uri.EscapeDataString(accountCredential["Password"]);
            var redirectUri = Uri.EscapeDataString(accountCredential["redirectUri"]);

            var appScope = "external";
            var scopeEndoded = Uri.EscapeDataString("openid ssosa profile email");
            var responseType = Uri.EscapeDataString("id_token token");
            var state = Uri.EscapeDataString(DateTime.Now + "" + DateTime.Now.Millisecond);
            var nonce = "xyz";

            var identityEndPoint = this._configSettingsBase.Value.AuthorityEndPoint;
            if (identityEndPoint.IsNull())
                throw new InvalidOperationException("Endpoint invalid");


            var disco = await DiscoveryClient.GetAsync(identityEndPoint);
            var tokenClient = new TokenClient(disco.TokenEndpoint, appClientId, appClientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(appScope);

            if (!tokenResponse.IsError)
            {
                var userServices = this._userServices;
                var userClientId = "seed-spa";
                var userLoged = await userServices.Auth(user, password);

                if (userLoged.IsNotNull())
                {
                    if (userLoged.Error.IsNullOrEmpty())
                    {
                        await _events.RaiseAsync(new UserLoginSuccessEvent(user, userLoged.SubjectId, user));
                        await HttpContext.SignInAsync(userLoged.SubjectId, user, userLoged.Claims.ToArray());

                        var url = string.Format("/connect/authorize/callback?client_id={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}&nonce={5}", userClientId, redirectUri, responseType, scopeEndoded, state, nonce);
                        return Redirect(url);
                    }
                }

                ModelState.AddModelError("Auth External User", userLoged.Error);
                return View("Error");
            }

            ModelState.AddModelError("Auth External APP", tokenResponse.Error);
            return View("Error");
        }



        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }


    }
}
