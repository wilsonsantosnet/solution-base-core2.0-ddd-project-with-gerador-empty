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
    public class SampleServiceBase : ServiceBase<Sample>
    {
        protected readonly ISampleRepository _rep;

        public SampleServiceBase(ISampleRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<Sample> GetOne(SampleFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<Sample>> GetByFilters(SampleFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<Sample>> GetByFiltersPaging(SampleFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(Sample sample)
        {
            this._rep.Remove(sample);
        }

        public virtual Summary GetSummary(PaginateResult<Sample> paginateResult)
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

        public override async Task<Sample> Save(Sample sample, bool questionToContinue = false)
        {
			var sampleOld = await this.GetOne(new SampleFilter { SampleId = sample.SampleId, QueryOptimizerBehavior = "OLD" });
			var sampleOrchestrated = await this.DomainOrchestration(sample, sampleOld);

            if (questionToContinue)
            {
                if (this.Continue(sampleOrchestrated, sampleOld) == false)
                    return sampleOrchestrated;
            }

            return this.SaveWithValidation(sampleOrchestrated, sampleOld);
        }

        public override async Task<Sample> SavePartial(Sample sample, bool questionToContinue = false)
        {
            var sampleOld = await this.GetOne(new SampleFilter { SampleId = sample.SampleId, QueryOptimizerBehavior = "OLD" });
			var sampleOrchestrated = await this.DomainOrchestration(sample, sampleOld);

            if (questionToContinue)
            {
                if (this.Continue(sampleOrchestrated, sampleOld) == false)
                    return sampleOrchestrated;
            }

            return SaveWithOutValidation(sampleOrchestrated, sampleOld);
        }

        protected override Sample SaveWithOutValidation(Sample sample, Sample sampleOld)
        {
            sample = this.SaveDefault(sample, sampleOld);
			this._cacheHelper.ClearCache();

			if (!sample.IsValid())
			{
				this._validationResult = sample.GetDomainValidation();
				this._validationWarning = sample.GetDomainWarning();
				return sample;
			}

            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Alterado com sucesso."
            };
            
            return sample;
        }

		protected override Sample SaveWithValidation(Sample sample, Sample sampleOld)
        {
            if (!this.IsValid(sample))
				return sample;
            
            sample = this.SaveDefault(sample, sampleOld);
            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Inserido com sucesso."
            };

            this._cacheHelper.ClearCache();
            return sample;
        }
		
		protected virtual bool IsValid(Sample entity)
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

		protected virtual void Specifications(Sample sample)
        {
            this._validationResult  = this._validationResult.Merge(new SampleIsSuitableValidation(this._rep).Validate(sample));
			this._validationWarning  = this._validationWarning.Merge(new SampleIsSuitableWarning(this._rep).Validate(sample));
        }

        protected virtual Sample SaveDefault(Sample sample, Sample sampleOld)
        {
			sample = this.AuditDefault(sample, sampleOld);

            var isNew = sampleOld.IsNull();			
            if (isNew)
                sample = this.AddDefault(sample);
            else
				sample = this.UpdateDefault(sample);

            return sample;
        }
		
        protected virtual Sample AddDefault(Sample sample)
        {
            sample = this._rep.Add(sample);
            return sample;
        }

		protected virtual Sample UpdateDefault(Sample sample)
        {
            sample = this._rep.Update(sample);
            return sample;
        }
				
		public virtual async Task<Sample> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Sample.SampleFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<Sample> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Sample.SampleFactory().GetDefaultInstance(model, user);
            });
         }
    }
}
