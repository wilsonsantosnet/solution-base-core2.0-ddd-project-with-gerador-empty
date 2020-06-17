using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Common.API
{

    public abstract class ControllerBase<T1> : Controller where T1: DtoBase
    {

        protected readonly IApplicationServiceBase<T1> _app;
        protected readonly ILogger _logger;
        protected readonly IHostingEnvironment _env;
        protected readonly ErrorMap _err;
      
        public ControllerBase(IApplicationServiceBase<T1> app, ILoggerFactory logger, IHostingEnvironment env, ErrorMap err)
        {
            this._app = app;
			this._logger = logger.CreateLogger<Controller>();
			this._env = env;
            this._err = err;
        }


        protected virtual async Task<IActionResult> Get<T2>(T2 filters, string errorMessage) where  T2 : FilterBase
        {
            var result = new HttpResult<T1>(this._logger, this._err);
            try
            {
                var searchResult = await this._app.GetByFilters(filters);
                return result.ReturnCustomResponse(this._app, searchResult, filters);


            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,errorMessage, filters);
				return responseEx;
            }

        }


		protected virtual async Task<IActionResult> GetOne<T2>(T2 filters, string errorMessage) where T2 : FilterBase
        {
			var result = new HttpResult<T1>(this._logger, this._err);
            try
            {
                var returnModel = await this._app.GetOne(filters);
                return result.ReturnCustomResponse(this._app, returnModel);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, errorMessage);
				return responseEx;
            }

		}



        protected virtual async Task<IActionResult> Post<T2>(T2 dto, string errorMessage) where T2: DtoBase
        {
            var result = new HttpResult<T1>(this._logger, this._err);
            try
            {
                var returnModel = await this._app.Save(dto as T1);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, errorMessage, dto);
				return responseEx;
            }
        }

        protected virtual async Task<IActionResult> Put<T2>(T2 dto, string errorMessage) where T2 : DtoBase
        {
            var result = new HttpResult<T1>(this._logger, this._err);
            try
            {
                var returnModel = await this._app.SavePartial(dto as T1);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, errorMessage, dto);
				return responseEx;
            }
        }


        protected virtual async Task<IActionResult> Delete<T2>(T2 dto, string errorMessage) where T2 : DtoBase
        {
            var result = new HttpResult<T1>(this._logger, this._err);
            try
            {
                await this._app.Remove(dto as T1);
                return result.ReturnCustomResponse(this._app, dto as T1);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, errorMessage, dto);
				return responseEx;
            }
        }

    }
}
