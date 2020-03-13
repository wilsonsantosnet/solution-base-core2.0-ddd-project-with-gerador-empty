using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using Seed.Domain.Interfaces.Repository;
using Seed.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seed.Domain.Services
{
    public class SampleTypeServiceBase : ServiceBase<SampleType>
    {
        protected readonly ISampleTypeRepository _rep;

        public SampleTypeServiceBase(ISampleTypeRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<SampleType> GetOne(SampleTypeFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<SampleType>> GetByFilters(SampleTypeFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<SampleType>> GetByFiltersPaging(SampleTypeFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(SampleType sampletype)
        {
            this._rep.Remove(sampletype);
        }

        public virtual Summary GetSummary(PaginateResult<SampleType> paginateResult)
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

        public override async Task<SampleType> Save(SampleType sampletype, bool questionToContinue = false)
        {
			var sampletypeOld = await this.GetOne(new SampleTypeFilter { SampleTypeId = sampletype.SampleTypeId, QueryOptimizerBehavior = "OLD" });
			var sampletypeOrchestrated = await this.DomainOrchestration(sampletype, sampletypeOld);

            if (questionToContinue)
            {
                if (this.Continue(sampletypeOrchestrated, sampletypeOld) == false)
                    return sampletypeOrchestrated;
            }

            return this.SaveWithValidation(sampletypeOrchestrated, sampletypeOld);
        }

        public override async Task<SampleType> SavePartial(SampleType sampletype, bool questionToContinue = false)
        {
            var sampletypeOld = await this.GetOne(new SampleTypeFilter { SampleTypeId = sampletype.SampleTypeId, QueryOptimizerBehavior = "OLD" });
			var sampletypeOrchestrated = await this.DomainOrchestration(sampletype, sampletypeOld);

            if (questionToContinue)
            {
                if (this.Continue(sampletypeOrchestrated, sampletypeOld) == false)
                    return sampletypeOrchestrated;
            }

            return SaveWithOutValidation(sampletypeOrchestrated, sampletypeOld);
        }

        protected override SampleType SaveWithOutValidation(SampleType sampletype, SampleType sampletypeOld)
        {
            sampletype = this.SaveDefault(sampletype, sampletypeOld);
			this._cacheHelper.ClearCache();

			if (!sampletype.IsValid())
			{
				this._validationResult = sampletype.GetDomainValidation();
				this._validationWarning = sampletype.GetDomainWarning();
				return sampletype;
			}

            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Alterado com sucesso."
            };
            
            return sampletype;
        }

		protected override SampleType SaveWithValidation(SampleType sampletype, SampleType sampletypeOld)
        {
            if (!this.IsValid(sampletype))
				return sampletype;
            
            sampletype = this.SaveDefault(sampletype, sampletypeOld);
            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Inserido com sucesso."
            };

            this._cacheHelper.ClearCache();
            return sampletype;
        }
		
		protected virtual bool IsValid(SampleType entity)
        {
            var isValid = true;
            if (!this.DataAnnotationIsValid() || !entity.IsValid())
            {
                if (this._validationResult.IsNull())
                    this._validationResult = entity.GetDomainValidation();
                else
                    this._validationResult.Merge(entity.GetDomainValidation());

                if (this._validationWarning.IsNull())
                    this._validationWarning = entity.GetDomainWarning();
                else
                    this._validationWarning.Merge(entity.GetDomainWarning());

                isValid = false;
            }

            this.Specifications(entity);
            if (!this._validationResult.IsValid)
                isValid = false;

            return isValid;
        }

		protected virtual void Specifications(SampleType sampletype)
        {
            this._validationResult  = this._validationResult.Merge(new SampleTypeIsSuitableValidation(this._rep).Validate(sampletype));
			this._validationWarning  = this._validationWarning.Merge(new SampleTypeIsSuitableWarning(this._rep).Validate(sampletype));
        }

        protected virtual SampleType SaveDefault(SampleType sampletype, SampleType sampletypeOld)
        {
			

            var isNew = sampletypeOld.IsNull();			
            if (isNew)
                sampletype = this.AddDefault(sampletype);
            else
				sampletype = this.UpdateDefault(sampletype);

            return sampletype;
        }
		
        protected virtual SampleType AddDefault(SampleType sampletype)
        {
            sampletype = this._rep.Add(sampletype);
            return sampletype;
        }

		protected virtual SampleType UpdateDefault(SampleType sampletype)
        {
            sampletype = this._rep.Update(sampletype);
            return sampletype;
        }
				
		public virtual async Task<SampleType> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new SampleType.SampleTypeFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<SampleType> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new SampleType.SampleTypeFactory().GetDefaultInstance(model, user);
            });
         }
    }
}
