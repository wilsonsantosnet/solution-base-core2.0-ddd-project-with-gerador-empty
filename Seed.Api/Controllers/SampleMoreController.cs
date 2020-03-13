using Common.API;
using Common.API.Extensions;
using Common.Domain.Base;
using Common.Domain.Enums;
using Common.Domain.Model;
using Common.Domain.CompositeKey;
using Common.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Seed.Application.Interfaces;
using Seed.CrossCuting;
using Seed.Domain.Filter;
using Seed.Domain.Interfaces.Repository;
using Seed.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Seed.Api.Controllers
{
	[Authorize]
    [Route("api/sample/more")]
    public class SampleMoreController : Controller
    {

        private readonly ISampleRepository _rep;
        private readonly ISampleApplicationService _app;
		private readonly ILogger _logger;
		private readonly ICache _cache;
		private readonly CacheHelper _cacheHelper;
		private readonly EnviromentInfo _env;
		private readonly CurrentUser _user;

        public SampleMoreController(ISampleRepository rep, ISampleApplicationService app, ILoggerFactory logger, EnviromentInfo env, CurrentUser user, ICache cache)
        {
            this._rep = rep;
            this._app = app;
			this._logger = logger.CreateLogger<SampleMoreController>();
			this._env = env;
			this._user = user;
			this._cache = cache;
			this._cacheHelper = new CacheHelper(this._cache);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]SampleFilter filters)
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {

				if (filters.FilterBehavior == FilterBehavior.GetDataItem)
				{
					if (this._user.GetClaims().GetTools().VerifyClaimsCanReadDataItem("Sample"))
					{
							var searchResult = await this._rep.GetDataItem(filters);
							return result.ReturnCustomResponse(searchResult, filters);
					}
					else
					{
						return new ObjectResult(null)
						{
							StatusCode = (int)HttpStatusCode.Forbidden
						};
					}
				}
				else if (this._user.GetClaims().GetTools().VerifyClaimsCanReadCustom("Sample"))
                {
					if (filters.FilterBehavior == FilterBehavior.GetDataCustom)
					{
						var searchResult = await this._rep.GetDataCustom(filters);
						return result.ReturnCustomResponse(searchResult, filters);
					}

					if (filters.FilterBehavior == FilterBehavior.GetDataListCustom)
					{
						var filterKey = filters.CompositeKey(this._user);
						if (filters.ByCache)
							if (this._cache.ExistsKey(filterKey))
								return result.ReturnCustomResponse(this._cache.Get<IEnumerable<object>>(filterKey), filters);

						var searchResult = await this._rep.GetDataListCustom(filters);
						this.AddCache(filters, filterKey, searchResult, "Sample");
						return result.ReturnCustomResponse(searchResult, filters);
					}

					if (filters.FilterBehavior == FilterBehavior.GetDataListCustomPaging)
					{
						var filterKey = filters.CompositeKey(this._user);
						if (filters.ByCache)
							if (this._cache.ExistsKey(filterKey))
								return result.ReturnCustomResponse(this._cache.Get<IEnumerable<object>>(filterKey), filters);

						var paginatedResult = await this._rep.GetDataListCustomPaging(filters);
						this.AddCache(filters, filterKey, paginatedResult.ResultPaginatedData, "Sample");
						return result.ReturnCustomResponse(paginatedResult.ToSearchResult<dynamic>(), filters);
					}

				
					if (filters.FilterBehavior == FilterBehavior.Export)
					{
						var searchResult = await this._rep.GetDataListCustom(filters);
						var export = new ExportExcelCustom<dynamic>(filters);
						var file = export.ExportFile(this.Response, searchResult, "Sample", this._env.RootPath);
						return File(file, export.ContentTypeExcel(), export.GetFileName());
					}

                }
				else
				{
                    return new ObjectResult(null)
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden
                    };
				}

				throw new InvalidOperationException("invalid FilterBehavior");

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", filters);
				return responseEx;
            }
        }

		[Authorize(Policy = "CanWrite")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IEnumerable<SampleDtoSpecialized> dtos)
        {
            var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
                var returnModels = await this._app.Save(dtos);
                return result.ReturnCustomResponse(this._app, returnModels);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", dtos);
				return responseEx;
            }

        }
		
		[Authorize(Policy = "CanWrite")]
		[HttpPut]
        public async Task<IActionResult> Put([FromBody]IEnumerable<SampleDtoSpecialized> dtos)
        {
            var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
                var returnModels = await this._app.SavePartial(dtos);
                return result.ReturnCustomResponse(this._app, returnModels);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, "Seed - Sample", dtos);
				return responseEx;
            }

        }

		private void AddCache(FilterBase filters, string filterKey, IEnumerable<dynamic> searchResult, string group)
        {
            if (filters.ByCache)
            {
                this._cache.Add(filterKey, searchResult, filters.CacheExpiresTime);
                this._cacheHelper.AddTagCache(filterKey, group);
            }
        }


    }
}
