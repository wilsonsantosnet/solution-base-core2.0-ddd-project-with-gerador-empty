using Common.API;
using Common.Domain.Base;
using IdentityModel.Client;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sso.Server.Api.Model;
using System;
using System.Threading.Tasks;

namespace Sso.Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthAppController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOptions<ConfigSettingsBase> _configSettingsBase;
        private readonly IEventService _events;
        private readonly IUserCredentialServices _userServices;

        public AuthAppController(ILoggerFactory logger, IOptions<ConfigSettingsBase> configSettingsBase, IEventService events, IUserCredentialServices userServices)
        {
            this._logger = logger.CreateLogger<AuthController>();
            this._configSettingsBase = configSettingsBase;
            this._logger.LogInformation("AccountController init success");
            this._events = events;
            this._userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AccountCredential accountCredential)
        {

            var result = new HttpResult<TokenResponse>(this._logger);

            var identityEndPoint = this._configSettingsBase.Value.AuthorityEndPoint;
            if (identityEndPoint.IsNull())
                throw new InvalidOperationException("Endpoint invalid");

            var tokenClient = new TokenClient(identityEndPoint + "/connect/token", accountCredential.ClientId, accountCredential.ClientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("ssosa");

            if (tokenResponse.IsError)
                return result.ReturnCustomException(new Exception(tokenResponse.Error), "Sso.Server.Api - Account");

            return result.ReturnCustomResponse(tokenResponse);

        }


    }
}
