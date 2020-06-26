using Common.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Seed.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CacheClearController : Controller
    {
        private readonly ICache _cache;

        public CacheClearController(ICache  cache)
        {
            this._cache = cache;
        }

        [HttpGet]
        public string Get(string key)
        {
            if (this._cache.ExistsKey(key))
            {
                this._cache.Remove(key);
                return "Success";
            }

            this._cache.Remove(key);
            return "Not Found";
        }
    }
}
