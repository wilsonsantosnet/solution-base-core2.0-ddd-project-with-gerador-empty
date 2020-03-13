using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Dto;
using Seed.Application.Interfaces;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using Seed.Domain.Interfaces.Services;
using Seed.Dto;
using System.Threading.Tasks;
using Common.Domain.Model;
using System.Collections.Generic;
using AutoMapper;

namespace Seed.Application
{
    public class SampleApplicationServiceBase : ApplicationServiceBase<Sample, SampleDto, SampleFilter>, ISampleApplicationService
    {
        protected readonly ValidatorAnnotations<SampleDto> _validatorAnnotations;
        protected readonly ISampleService _service;
		protected readonly CurrentUser _user;

        public SampleApplicationServiceBase(ISampleService service, IUnitOfWork uow, ICache cache, CurrentUser user, IMapper mapper) :
            base(service, uow, cache, mapper)
        {
            base.SetTagNameCache("Sample");
            this._validatorAnnotations = new ValidatorAnnotations<SampleDto>();
            this._service = service;
			this._user = user;
        }

       protected override async Task<Sample> MapperDtoToDomain<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as SampleDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = this._service.GetNewInstance(_dto, this._user);
				return domain;
			});
        }

		protected override async Task<IEnumerable<Sample>> MapperDtoToDomain<TDS>(IEnumerable<TDS> dtos)
        {
			var domains = new List<Sample>();
			foreach (var dto in dtos)
			{
				var _dto = dto as SampleDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = await this._service.GetNewInstance(_dto, this._user);
				domains.Add(domain);
			}
			return domains;
			
        }


        protected override async Task<Sample> AlterDomainWithDto<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as SampleDto;
				var domain = this._service.GetUpdateInstance(_dto, this._user);
				return domain;
			});
        }



    }
}
