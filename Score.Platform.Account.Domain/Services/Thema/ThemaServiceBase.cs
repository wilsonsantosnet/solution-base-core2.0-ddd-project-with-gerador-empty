using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using Score.Platform.Account.Domain.Interfaces.Repository;
using Score.Platform.Account.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Score.Platform.Account.Domain.Services
{
    public class ThemaServiceBase : ServiceBase<Thema>
    {
        protected readonly IThemaRepository _rep;

        public ThemaServiceBase(IThemaRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<Thema> GetOne(ThemaFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<Thema>> GetByFilters(ThemaFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<Thema>> GetByFiltersPaging(ThemaFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(Thema thema)
        {
            this._rep.Remove(thema);
        }

        public virtual Summary GetSummary(PaginateResult<Thema> paginateResult)
        {
            return new Summary
            {
                Total = paginateResult.TotalCount,
				PageSize = paginateResult.PageSize,
            };
        }

        public virtual ValidationSpecificationResult GetDomainValidation(FilterBase filters = null)
        {
            return this._validationResult;
        }

        public virtual ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null)
        {
            return this._validationConfirm;
        }

        public virtual WarningSpecificationResult GetDomainWarning(FilterBase filters = null)
        {
            return this._validationWarning;
        }

        public override async Task<Thema> Save(Thema thema, bool questionToContinue = false)
        {
			var themaOld = await this.GetOne(new ThemaFilter { ThemaId = thema.ThemaId });
			var themaOrchestrated = await this.DomainOrchestration(thema, themaOld);

            if (questionToContinue)
            {
                if (this.Continue(themaOrchestrated, themaOld) == false)
                    return themaOrchestrated;
            }

            return this.SaveWithValidation(themaOrchestrated, themaOld);
        }

        public override async Task<Thema> SavePartial(Thema thema, bool questionToContinue = false)
        {
            var themaOld = await this.GetOne(new ThemaFilter { ThemaId = thema.ThemaId });
			var themaOrchestrated = await this.DomainOrchestration(thema, themaOld);

            if (questionToContinue)
            {
                if (this.Continue(themaOrchestrated, themaOld) == false)
                    return themaOrchestrated;
            }

            return SaveWithOutValidation(themaOrchestrated, themaOld);
        }

        protected override Thema SaveWithOutValidation(Thema thema, Thema themaOld)
        {
            thema = this.SaveDefault(thema, themaOld);
			this._cacheHelper.ClearCache();

			if (!thema.IsValid())
			{
				this._validationResult = thema.GetDomainValidation();
				this._validationWarning = thema.GetDomainWarning();
				return thema;
			}

            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Alterado com sucesso."
            };
            
            return thema;
        }

		protected override Thema SaveWithValidation(Thema thema, Thema themaOld)
        {
            if (!this.IsValid(thema))
				return thema;
            
            thema = this.SaveDefault(thema, themaOld);
            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Inserido com sucesso."
            };

            this._cacheHelper.ClearCache();
            return thema;
        }
		
		protected virtual bool IsValid(Thema entity)
        {
            var isValid = true;
            if (!this.DataAnnotationIsValid())
            {
                this._validationResult = entity.GetDomainValidation();
                this._validationWarning = entity.GetDomainWarning();
                isValid = false;
            }

            if (!entity.IsValid())
            {
                this._validationResult = entity.GetDomainValidation();
                this._validationWarning = entity.GetDomainWarning();
                isValid = false;
            }

            this.Specifications(entity);
            if (!this._validationResult.IsValid)
                isValid = false;

            return isValid;
        }

		protected virtual void Specifications(Thema thema)
        {
            this._validationResult  = this._validationResult.Merge(new ThemaIsSuitableValidation(this._rep).Validate(thema));
			this._validationWarning  = this._validationWarning.Merge(new ThemaIsSuitableWarning(this._rep).Validate(thema));
        }

        protected virtual Thema SaveDefault(Thema thema, Thema themaOld)
        {
			

            var isNew = themaOld.IsNull();			
            if (isNew)
                thema = this.AddDefault(thema);
            else
				thema = this.UpdateDefault(thema);

            return thema;
        }
		
        protected virtual Thema AddDefault(Thema thema)
        {
            thema = this._rep.Add(thema);
            return thema;
        }

		protected virtual Thema UpdateDefault(Thema thema)
        {
            thema = this._rep.Update(thema);
            return thema;
        }
				
		public virtual async Task<Thema> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Thema.ThemaFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<Thema> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Thema.ThemaFactory().GetDefaultInstance(model, user);
            });
         }
    }
}
