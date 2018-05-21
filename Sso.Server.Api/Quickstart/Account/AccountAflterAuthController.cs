using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4.Quickstart.UI
{
    [Authorize]
    [Route("[controller]")]
    public class AccountAfterAuthController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public AccountAfterAuthController(
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            _interaction = interaction;
            _events = events;

        }


        [HttpGet("ClaimsAdd")]
        public async Task<IActionResult> ClaimsAdd(string claims_type, string claims_value, string client_id, string redirect_uri, string response_type, string scope, string state)
        {
            var userClaims = HttpContext.User.Claims.ToList();
            var userName = userClaims.Where(_ => _.Type == "name").Select(_ => _.Value).SingleOrDefault();
            var subjectId = userClaims.Where(_ => _.Type == "sub").Select(_ => _.Value).SingleOrDefault();

            this.AddClaims(claims_type, claims_value, userClaims);

            await _events.RaiseAsync(new UserLoginSuccessEvent(userName, subjectId, userName));
            await HttpContext.SignInAsync(subjectId, userName, userClaims.ToArray());

            var url = string.Format("/connect/authorize/login?client_id={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}", client_id, redirect_uri, response_type, scope, state);
            return Redirect(url);

        }

        private void AddClaims(string claims_type, string claims_value, List<Claim> userClaims)
        {
            var existsClaims = userClaims.Where(_ => _.Type == claims_type).SingleOrDefault();
            if (existsClaims.IsNotNull())
                userClaims.Remove(existsClaims);
            
            userClaims.Add(new Claim(claims_type, claims_value));
        }
    }
}
