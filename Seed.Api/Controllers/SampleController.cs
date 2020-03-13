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
    public class SampleController : Controller
    {

        private readonly ISampleApplicationService _app;
		private readonly ILogger _logger;
		private readonly IHostingEnvironment _env;
      
        public SampleController(ISampleApplicationService app, ILoggerFactory logger, IHostingEnvironment env)
        {
            this._app = app;
			this._logger = logger.CreateLogger<SampleController>();
			this._env = env;
        }

		[Authorize(Policy = "CanReadAll")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]SampleFilter filters)
        {
            var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
                var searchResult = await this._app.GetByFilters(filters);
                return result.ReturnCustomResponse(this._app, searchResult, filters);


            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", filters);
				return responseEx;
            }

        }


		[HttpGet("{id}")]
		[Authorize(Policy = "CanReadOne")]
		public async Task<IActionResult> Get(int id, [FromQuery]SampleFilter filters)
		{
			var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
				if (id.IsSent()) filters.SampleId = id;
                var returnModel = await this._app.GetOne(filters);
                return result.ReturnCustomResponse(this._app, returnModel);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", id);
				return responseEx;
            }

		}



        [Authorize(Policy = "CanSave")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SampleDtoSpecialized dto)
        {
            var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
                var returnModel = await this._app.Save(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", dto);
				return responseEx;
            }
        }


		[Authorize(Policy = "CanEdit")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]SampleDtoSpecialized dto)
        {
            var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
                var returnModel = await this._app.SavePartial(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", dto);
				return responseEx;
            }
        }

		[Authorize(Policy = "CanDelete")]
        [HttpDelete]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, SampleDto dto)
        {
            var result = new HttpResult<SampleDto>(this._logger, new ErrorMapCustom());
            try
            {
				if (id.IsSent()) dto.SampleId = id;
                await this._app.Remove(dto);
                return result.ReturnCustomResponse(this._app, dto);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex,"Seed - Sample", dto);
				return responseEx;
            }
        }



    }
}
