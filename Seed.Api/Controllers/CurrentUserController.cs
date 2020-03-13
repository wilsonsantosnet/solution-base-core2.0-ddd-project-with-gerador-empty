using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Common.Domain.Model;
using Common.API;
using System.Threading.Tasks;

namespace Seed.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CurrentUserController : Controller
    {

        private readonly CurrentUser _user;
		private readonly ILogger _logger;

        public CurrentUserController(CurrentUser user, ILoggerFactory logger)
        {
            this._user = user;
			this._logger = logger.CreateLogger<CurrentUserController>();
			this._logger.LogInformation("AccountController init success");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {
                return await Task.Run(() =>
                {
                    var claims = this._user.GetClaims();
                    if (claims.IsAny())
                    {
                        return result.ReturnCustomResponse(claims);
                    }

                    return result.ReturnCustomResponse(new { warning = "No Claims found!" });

                });

            }
            catch (Exception ex)
            {
                 var responseEx = result.ReturnCustomException(ex, "Seed - CurrentUser");
				 return responseEx;
            }

        }
	
		[HttpGet("IsAuth")]
        public async Task<IActionResult> IsAuth()
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {
                return await Task.Run(() =>
                {
                    var claims = this._user.GetClaims();
                    if (claims.IsAny())
                    {
                        return result.ReturnCustomResponse(new { claimsCount = claims.Count() });
                    }

                    return result.ReturnCustomResponse(new { claimsCount = 0 });

                });

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex, "Seed - CurrentUser");
            }

        }

    }
}
