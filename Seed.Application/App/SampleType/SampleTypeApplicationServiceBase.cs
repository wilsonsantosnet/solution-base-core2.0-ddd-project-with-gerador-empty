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
    public class SampleTypeApplicationServiceBase : ApplicationServiceBase<SampleType, SampleTypeDto, SampleTypeFilter>, ISampleTypeApplicationService
    {
        protected readonly ValidatorAnnotations<SampleTypeDto> _validatorAnnotations;
        protected readonly ISampleTypeService _service;
		protected readonly CurrentUser _user;

        public SampleTypeApplicationServiceBase(ISampleTypeService service, IUnitOfWork uow, ICache cache, CurrentUser user, IMapper mapper) :
            base(service, uow, cache, mapper)
        {
            base.SetTagNameCache("SampleType");
            this._validatorAnnotations = new ValidatorAnnotations<SampleTypeDto>();
            this._service = service;
			this._user = user;
        }

       protected override async Task<SampleType> MapperDtoToDomain<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as SampleTypeDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = this._service.GetNewInstance(_dto, this._user);
				return domain;
			});
        }

		protected override async Task<IEnumerable<SampleType>> MapperDtoToDomain<TDS>(IEnumerable<TDS> dtos)
        {
			var domains = new List<SampleType>();
			foreach (var dto in dtos)
			{
				var _dto = dto as SampleTypeDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = await this._service.GetNewInstance(_dto, this._user);
				domains.Add(domain);
			}
			return domains;
			
        }


        protected override async Task<SampleType> AlterDomainWithDto<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as SampleTypeDto;
				var domain = this._service.GetUpdateInstance(_dto, this._user);
				return domain;
			});
        }



    }
}
