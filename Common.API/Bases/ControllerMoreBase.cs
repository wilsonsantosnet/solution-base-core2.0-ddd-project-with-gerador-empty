using Common.API;
using Common.API.Extensions;
using Common.Domain.Base;
using Common.Domain.Enums;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace Common.API
{

    public abstract class ControllerMoreBase<TDto, TFilter, TEntity>
        : Controller where TDto : DtoBase where TFilter : FilterBase
    {

        protected readonly IRepositoryExtensions<TEntity, TFilter> _rep;
        protected readonly IApplicationServiceBase<TDto> _app;
        protected readonly ILogger _logger;
        protected readonly ICache _cache;
        protected readonly CacheHelper _cacheHelper;
        protected readonly EnviromentInfo _env;
        protected readonly CurrentUser _user;
        protected readonly ExportExcel<dynamic> _export;
        protected readonly ErrorMap _err;

        public ControllerMoreBase(IRepositoryExtensions<TEntity, TFilter> rep, IApplicationServiceBase<TDto> app, ILoggerFactory logger, EnviromentInfo env, CurrentUser user, ICache cache, ExportExcel<dynamic> export, ErrorMap err)
        {
            this._rep = rep;
            this._app = app;
            this._logger = logger.CreateLogger<Controller>();
            this._env = env;
            this._user = user;
            this._cache = cache;
            this._cacheHelper = new CacheHelper(this._cache);
            this._export = export;
            this._err = err;
        }


        protected async Task<IActionResult> Get(TFilter filters, string EntityName, string errorMessage)
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {

                if (filters.FilterBehavior == FilterBehavior.GetDataItem)
                {
                    if (this._user.GetClaims().GetTools().VerifyClaimsCanReadDataItem(EntityName))
                    {
                        var filterKey = filters.CompositeKey(this._user);
                        if (filters.ByCache)
                        {
                            var cacheResult = this._cache.Get<IEnumerable<object>>(filterKey);
                            if (cacheResult.IsNotNull())
                            {
                                filters.FilterKey = filterKey;
                                return result.ReturnCustomResponse(cacheResult, filters);
                            }
                        }

                        var searchResult = await this._rep.GetDataItem(filters);
                        this.AddCache(filters, filterKey, searchResult, EntityName);
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
                else if (this._user.GetClaims().GetTools().VerifyClaimsCanReadCustom(EntityName))
                {
                    if (filters.FilterBehavior == FilterBehavior.GetDataCustom)
                    {
                        var filterKey = filters.CompositeKey(this._user);
                        if (filters.ByCache)
                        {
                            var cacheResult = this._cache.Get<object>(filterKey);
                            if (cacheResult.IsNotNull())
                            {
                                filters.FilterKey = filterKey;
                                return result.ReturnCustomResponse(cacheResult, filters);
                            }
                        }

                        var searchResult = await this._rep.GetDataCustom(filters);
                        this.AddCache(filters, filterKey, searchResult, EntityName);
                        return result.ReturnCustomResponse(searchResult, filters);
                    }

                    if (filters.FilterBehavior == FilterBehavior.GetDataListCustom)
                    {
                        var filterKey = filters.CompositeKey(this._user);
                        if (filters.ByCache)
                        {
                            var cacheResult = this._cache.Get<IEnumerable<object>>(filterKey);
                            if (cacheResult.IsNotNull())
                            {
                                filters.FilterKey = filterKey;
                                return result.ReturnCustomResponse(cacheResult, filters);
                            }
                        }

                        var searchResult = await this._rep.GetDataListCustom(filters);
                        this.AddCache(filters, filterKey, searchResult, EntityName);
                        return result.ReturnCustomResponse(searchResult, filters);
                    }

                    if (filters.FilterBehavior == FilterBehavior.GetDataListCustomPaging)
                    {
                        var filterKey = filters.CompositeKey(this._user);
                        if (filters.ByCache)
                        {
                            var cacheResult = this._cache.Get<SearchResult<dynamic>>(filterKey);
                            if (cacheResult.IsNotNull())
                            {
                                filters.FilterKey = filterKey;
                                return result.ReturnCustomResponse(cacheResult, filters);
                            }
                        }

                        var paginatedResult = await this._rep.GetDataListCustomPaging(filters);
                        this.AddCache(filters, filterKey, paginatedResult.ResultPaginatedData, EntityName);
                        return result.ReturnCustomResponse(paginatedResult.ToSearchResult<dynamic>(), filters);
                    }


                    if (filters.FilterBehavior == FilterBehavior.Export)
                    {
                        var searchResult = await this._rep.GetDataListCustom(filters);
                        var file = this._export.ExportFile(this.Response, searchResult, EntityName, this._env.RootPath);
                        return File(file, this._export.ContentTypeExcel(), this._export.GetFileName());
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
                var responseEx = result.ReturnCustomException(ex, errorMessage, filters);
                return responseEx;
            }
        }

        protected async Task<IActionResult> Post<T2>(IEnumerable<T2> dtos, string errorMessage) where T2 : DtoBase
        {
            var result = new HttpResult<TDto>(this._logger, this._err);
            try
            {
                var returnModels = await this._app.Save(dtos as IEnumerable<TDto>);
                return result.ReturnCustomResponse(this._app, returnModels);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, errorMessage, dtos);
                return responseEx;
            }

        }

        protected async Task<IActionResult> Put<T2>(IEnumerable<T2> dtos, string errorMessage) where T2 : DtoBase
        {
            var result = new HttpResult<TDto>(this._logger, this._err);
            try
            {
                var returnModels = await this._app.SavePartial(dtos as IEnumerable<TDto>);
                return result.ReturnCustomResponse(this._app, returnModels);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, errorMessage, dtos);
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
