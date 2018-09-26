using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Dto;
using Score.Platform.Account.Application.Interfaces;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using Score.Platform.Account.Domain.Interfaces.Services;
using Score.Platform.Account.Dto;
using System.Threading.Tasks;
using Common.Domain.Model;
using System.Collections.Generic;

namespace Score.Platform.Account.Application
{
    public class TenantApplicationServiceBase : ApplicationServiceBase<Tenant, TenantDto, TenantFilter>, ITenantApplicationService
    {
        protected readonly ValidatorAnnotations<TenantDto> _validatorAnnotations;
        protected readonly ITenantService _service;
		protected readonly CurrentUser _user;

        public TenantApplicationServiceBase(ITenantService service, IUnitOfWork uow, ICache cache, CurrentUser user) :
            base(service, uow, cache)
        {
            base.SetTagNameCache("Tenant");
            this._validatorAnnotations = new ValidatorAnnotations<TenantDto>();
            this._service = service;
			this._user = user;
        }

       protected override async Task<Tenant> MapperDtoToDomain<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as TenantDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = this._service.GetNewInstance(_dto, this._user);
				return domain;
			});
        }

		protected override async Task<IEnumerable<Tenant>> MapperDtoToDomain<TDS>(IEnumerable<TDS> dtos)
        {
			var domains = new List<Tenant>();
			foreach (var dto in dtos)
			{
				var _dto = dto as TenantDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = await this._service.GetNewInstance(_dto, this._user);
				domains.Add(domain);
			}
			return domains;
			
        }


        protected override async Task<Tenant> AlterDomainWithDto<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as TenantDto;
				var domain = this._service.GetUpdateInstance(_dto, this._user);
				return domain;
			});
        }



    }
}
