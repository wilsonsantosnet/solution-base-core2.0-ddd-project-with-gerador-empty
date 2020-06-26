using Common.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Seed.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CacheController : Controller
    {
        private readonly ICache _cache;

        public CacheController(ICache  cache)
        {
            this._cache = cache;
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            var result = this._cache.Get<IEnumerable<object>>(key);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
