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
    public class ThemaApplicationServiceBase : ApplicationServiceBase<Thema, ThemaDto, ThemaFilter>, IThemaApplicationService
    {
        protected readonly ValidatorAnnotations<ThemaDto> _validatorAnnotations;
        protected readonly IThemaService _service;
		protected readonly CurrentUser _user;

        public ThemaApplicationServiceBase(IThemaService service, IUnitOfWork uow, ICache cache, CurrentUser user) :
            base(service, uow, cache)
        {
            base.SetTagNameCache("Thema");
            this._validatorAnnotations = new ValidatorAnnotations<ThemaDto>();
            this._service = service;
			this._user = user;
        }

       protected override async Task<Thema> MapperDtoToDomain<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as ThemaDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = this._service.GetNewInstance(_dto, this._user);
				return domain;
			});
        }

		protected override async Task<IEnumerable<Thema>> MapperDtoToDomain<TDS>(IEnumerable<TDS> dtos)
        {
			var domains = new List<Thema>();
			foreach (var dto in dtos)
			{
				var _dto = dto as ThemaDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = await this._service.GetNewInstance(_dto, this._user);
				domains.Add(domain);
			}
			return domains;
			
        }


        protected override async Task<Thema> AlterDomainWithDto<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as ThemaDto;
				var domain = this._service.GetUpdateInstance(_dto, this._user);
				return domain;
			});
        }



    }
}
