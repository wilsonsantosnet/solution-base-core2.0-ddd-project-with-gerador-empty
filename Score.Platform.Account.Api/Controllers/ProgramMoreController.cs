using Common.API;
using Common.API.Extensions;
using Common.Domain.Base;
using Common.Domain.Enums;
using Common.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Score.Platform.Account.Application.Interfaces;
using Score.Platform.Account.CrossCuting;
using Score.Platform.Account.Domain.Filter;
using Score.Platform.Account.Domain.Interfaces.Repository;
using Score.Platform.Account.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Score.Platform.Account.Api.Controllers
{
	[Authorize]
    [Route("api/program/more")]
    public class ProgramMoreController : Controller
    {

        private readonly IProgramRepository _rep;
        private readonly IProgramApplicationService _app;
		private readonly ILogger _logger;
		private readonly EnviromentInfo _env;
		private readonly CurrentUser _user;

        public ProgramMoreController(IProgramRepository rep, IProgramApplicationService app, ILoggerFactory logger, EnviromentInfo env,CurrentUser user)
        {
            this._rep = rep;
            this._app = app;
			this._logger = logger.CreateLogger<ProgramMoreController>();
			this._env = env;
			this._user = user;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ProgramFilter filters)
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {
                if (filters.FilterBehavior == FilterBehavior.GetDataItem)
                {
                    var searchResult = await this._rep.GetDataItem(filters);
                    return result.ReturnCustomResponse(searchResult, filters);
                }

				if (!this._user.GetClaims().GetTools().VerifyClaimsCanRead("Program"))
                {
                    return new ObjectResult(null)
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden
                    };
                }


                if (filters.FilterBehavior == FilterBehavior.GetDataCustom)
                {
                    var searchResult = await this._rep.GetDataCustom(filters);
                    return result.ReturnCustomResponse(searchResult, filters);
                }

                if (filters.FilterBehavior == FilterBehavior.GetDataListCustom)
                {
                    var searchResult = await this._rep.GetDataListCustom(filters);
                    return result.ReturnCustomResponse(searchResult, filters);
                }

				if (filters.FilterBehavior == FilterBehavior.GetDataListCustomPaging)
                {
                    var paginatedResult = await this._rep.GetDataListCustomPaging(filters);
                    return result.ReturnCustomResponse(paginatedResult.ToSearchResult<dynamic>(), filters);
                }

				
				if (filters.FilterBehavior == FilterBehavior.Export)
                {
					var searchResult = await this._rep.GetDataListCustom(filters);
                    var export = new ExportExcelCustom<dynamic>(filters);
                    var file = export.ExportFile(this.Response, searchResult, "Program", this._env.RootPath);
                    return File(file, export.ContentTypeExcel(), export.GetFileName());
                }

                throw new InvalidOperationException("invalid FilterBehavior");

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Program", filters, new ErrorMapCustom());
            }
        }

		[Authorize(Policy = "CanWrite")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IEnumerable<ProgramDtoSpecialized> dtos)
        {
            var result = new HttpResult<ProgramDto>(this._logger);
            try
            {
                var returnModels = await this._app.Save(dtos);
                return result.ReturnCustomResponse(this._app, returnModels);

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"Score.Platform.Account - Program", dtos, new ErrorMapCustom());
            }

        }
		
		[Authorize(Policy = "CanWrite")]
		[HttpPut]
        public async Task<IActionResult> Put([FromBody]IEnumerable<ProgramDtoSpecialized> dtos)
        {
            var result = new HttpResult<ProgramDto>(this._logger);
            try
            {
                var returnModels = await this._app.SavePartial(dtos);
                return result.ReturnCustomResponse(this._app, returnModels);

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex, "Score.Platform.Account - Program", dtos, new ErrorMapCustom());
            }

        }

    }
}
