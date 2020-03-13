using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Seed.Application.Interfaces;
using Seed.Domain.Filter;
using Seed.Dto;
using Common.API;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Seed.CrossCuting;

namespace Seed.Api.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    public class SampleTypeController : Controller
    {

        private readonly ISampleTypeApplicationService _app;
		private readonly ILogger _logger;
		private readonly IHostingEnvironment _env;
      
        public SampleTypeController(ISampleTypeApplicationService app, ILoggerFactory logger, IHostingEnvironment env)
        {
            this._app = app;
			this._logger = logger.CreateLogger<SampleTypeController>();
			this._env = env;
        }

		[Authorize(Policy = "CanReadAll")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]SampleTypeFilter filters)
        {
            var result = new HttpResult<SampleTypeDto>(this._logger, new ErrorMapCustom());
            try
            {
                var searchResult = await this._app.GetByFilters(filters);
                return result.ReturnCustomResponse(this._app, searchResult, filters);


            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - SampleType", filters);
				return responseEx;
            }

        }


		[HttpGet("{id}")]
		[Authorize(Policy = "CanReadOne")]
		public async Task<IActionResult> Get(int id, [FromQuery]SampleTypeFilter filters)
		{
			var result = new HttpResult<SampleTypeDto>(this._logger, new ErrorMapCustom());
            try
            {
				if (id.IsSent()) filters.SampleTypeId = id;
                var returnModel = await this._app.GetOne(filters);
                return result.ReturnCustomResponse(this._app, returnModel);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - SampleType", id);
				return responseEx;
            }

		}



        [Authorize(Policy = "CanSave")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SampleTypeDtoSpecialized dto)
        {
            var result = new HttpResult<SampleTypeDto>(this._logger, new ErrorMapCustom());
            try
            {
                var returnModel = await this._app.Save(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - SampleType", dto);
				return responseEx;
            }
        }


		[Authorize(Policy = "CanEdit")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]SampleTypeDtoSpecialized dto)
        {
            var result = new HttpResult<SampleTypeDto>(this._logger, new ErrorMapCustom());
            try
            {
                var returnModel = await this._app.SavePartial(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - SampleType", dto);
				return responseEx;
            }
        }

		[Authorize(Policy = "CanDelete")]
        [HttpDelete]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, SampleTypeDto dto)
        {
            var result = new HttpResult<SampleTypeDto>(this._logger, new ErrorMapCustom());
            try
            {
				if (id.IsSent()) dto.SampleTypeId = id;
                await this._app.Remove(dto);
                return result.ReturnCustomResponse(this._app, dto);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - SampleType", dto);
				return responseEx;
            }
        }



    }
}
