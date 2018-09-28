using System;
using Microsoft.AspNetCore.Mvc;
using Common.API;
using Microsoft.Extensions.Logging;
using Common.Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace Sso.Server.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserInfoController : Controller
    {
        private readonly CurrentUser _user;
        private ILogger _logger;
        public UserInfoController(CurrentUser user, ILoggerFactory logger)
        {
            this._user = user;
            this._logger = logger.CreateLogger<UserInfoController>();
            this._logger.LogInformation("UserInfoController init success");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {
                var claims = this._user.GetClaims();
                if (claims.IsAny())
                    return result.ReturnCustomResponse(claims);

                return result.ReturnCustomResponse(new { warning = "No Claims found!" });
            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex, "Sso - CurrentUser");
            }
        }
    }
}
