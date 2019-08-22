using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace Sso.Server.Api.Controllers
{
	
    [Route("api/[controller]")]
    public class HealthController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return string.Format("is live at now {0}, now with TimeZone :{1} in server Culture {2}, Decimal Sample: {3}", DateTime.Now, DateTime.Now.ToTimeZone(), CultureInfo.CurrentCulture, 10.58);
        }
    }
}
