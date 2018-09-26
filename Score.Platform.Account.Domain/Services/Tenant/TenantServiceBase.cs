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
    public class TenantServiceBase : ServiceBase<Tenant>
    {
        protected readonly ITenantRepository _rep;

        public TenantServiceBase(ITenantRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<Tenant> GetOne(TenantFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<Tenant>> GetByFilters(TenantFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<Tenant>> GetByFiltersPaging(TenantFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(Tenant tenant)
        {
            this._rep.Remove(tenant);
        }

        public virtual Summary GetSummary(PaginateResult<Tenant> paginateResult)
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

        public override async Task<Tenant> Save(Tenant tenant, bool questionToContinue = false)
        {
			var tenantOld = await this.GetOne(new TenantFilter { TenantId = tenant.TenantId });
			var tenantOrchestrated = await this.DomainOrchestration(tenant, tenantOld);

            if (questionToContinue)
            {
                if (this.Continue(tenantOrchestrated, tenantOld) == false)
                    return tenantOrchestrated;
            }

            return this.SaveWithValidation(tenantOrchestrated, tenantOld);
        }

        public override async Task<Tenant> SavePartial(Tenant tenant, bool questionToContinue = false)
        {
            var tenantOld = await this.GetOne(new TenantFilter { TenantId = tenant.TenantId });
			var tenantOrchestrated = await this.DomainOrchestration(tenant, tenantOld);

            if (questionToContinue)
            {
                if (this.Continue(tenantOrchestrated, tenantOld) == false)
                    return tenantOrchestrated;
            }

            return SaveWithOutValidation(tenantOrchestrated, tenantOld);
        }

        protected override Tenant SaveWithOutValidation(Tenant tenant, Tenant tenantOld)
        {
            tenant = this.SaveDefault(tenant, tenantOld);
			this._cacheHelper.ClearCache();

			if (!tenant.IsValid())
			{
				this._validationResult = tenant.GetDomainValidation();
				this._validationWarning = tenant.GetDomainWarning();
				return tenant;
			}

            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Alterado com sucesso."
            };
            
            return tenant;
        }

		protected override Tenant SaveWithValidation(Tenant tenant, Tenant tenantOld)
        {
            if (!this.IsValid(tenant))
				return tenant;
            
            tenant = this.SaveDefault(tenant, tenantOld);
            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Inserido com sucesso."
            };

            this._cacheHelper.ClearCache();
            return tenant;
        }
		
		protected virtual bool IsValid(Tenant entity)
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

		protected virtual void Specifications(Tenant tenant)
        {
            this._validationResult  = this._validationResult.Merge(new TenantIsSuitableValidation(this._rep).Validate(tenant));
			this._validationWarning  = this._validationWarning.Merge(new TenantIsSuitableWarning(this._rep).Validate(tenant));
        }

        protected virtual Tenant SaveDefault(Tenant tenant, Tenant tenantOld)
        {
			tenant = this.AuditDefault(tenant, tenantOld);

            var isNew = tenantOld.IsNull();			
            if (isNew)
                tenant = this.AddDefault(tenant);
            else
				tenant = this.UpdateDefault(tenant);

            return tenant;
        }
		
        protected virtual Tenant AddDefault(Tenant tenant)
        {
            tenant = this._rep.Add(tenant);
            return tenant;
        }

		protected virtual Tenant UpdateDefault(Tenant tenant)
        {
            tenant = this._rep.Update(tenant);
            return tenant;
        }
				
		public virtual async Task<Tenant> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Tenant.TenantFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<Tenant> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Tenant.TenantFactory().GetDefaultInstance(model, user);
            });
         }
    }
}
