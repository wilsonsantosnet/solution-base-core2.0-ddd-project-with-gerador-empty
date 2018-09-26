using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Score.Platform.Account.Application.Interfaces;
using Score.Platform.Account.Domain.Filter;
using Score.Platform.Account.Dto;
using Common.API;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Score.Platform.Account.CrossCuting;

namespace Score.Platform.Account.Api.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    public class TenantController : Controller
    {

        private readonly ITenantApplicationService _app;
		private readonly ILogger _logger;
		private readonly IHostingEnvironment _env;
      
        public TenantController(ITenantApplicationService app, ILoggerFactory logger, IHostingEnvironment env)
        {
            this._app = app;
			this._logger = logger.CreateLogger<TenantController>();
			this._env = env;
        }

		[Authorize(Policy = "CanRead")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]TenantFilter filters)
        {
            var result = new HttpResult<TenantDto>(this._logger);
            try
            {
                var searchResult = await this._app.GetByFilters(filters);
                return result.ReturnCustomResponse(this._app, searchResult, filters);


            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Tenant", filters, new ErrorMapCustom());
            }

        }


		[HttpGet("{id}")]
		[Authorize(Policy = "CanRead")]
		public async Task<IActionResult> Get(int id, [FromQuery]TenantFilter filters)
		{
			var result = new HttpResult<TenantDto>(this._logger);
            try
            {
				if (id.IsSent()) filters.TenantId = id;
                var returnModel = await this._app.GetOne(filters);
                return result.ReturnCustomResponse(this._app, returnModel);
            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Tenant", id);
            }

		}



        [Authorize(Policy = "CanWrite")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TenantDtoSpecialized dto)
        {
            var result = new HttpResult<TenantDto>(this._logger);
            try
            {
                var returnModel = await this._app.Save(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Tenant", dto, new ErrorMapCustom());
            }
        }


		[Authorize(Policy = "CanWrite")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]TenantDtoSpecialized dto)
        {
            var result = new HttpResult<TenantDto>(this._logger);
            try
            {
                var returnModel = await this._app.SavePartial(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Tenant", dto, new ErrorMapCustom());
            }
        }

		[Authorize(Policy = "CanDelete")]
        [HttpDelete]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, TenantDto dto)
        {
            var result = new HttpResult<TenantDto>(this._logger);
            try
            {
				if (id.IsSent()) dto.TenantId = id;
                await this._app.Remove(dto);
                return result.ReturnCustomResponse(this._app, dto);
            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Tenant", dto, new ErrorMapCustom());
            }
        }



    }
}
