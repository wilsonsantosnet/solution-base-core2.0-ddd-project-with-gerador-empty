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
    public class ProgramServiceBase : ServiceBase<Program>
    {
        protected readonly IProgramRepository _rep;

        public ProgramServiceBase(IProgramRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<Program> GetOne(ProgramFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<Program>> GetByFilters(ProgramFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<Program>> GetByFiltersPaging(ProgramFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(Program program)
        {
            this._rep.Remove(program);
        }

        public virtual Summary GetSummary(PaginateResult<Program> paginateResult)
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

        public override async Task<Program> Save(Program program, bool questionToContinue = false)
        {
			var programOld = await this.GetOne(new ProgramFilter { ProgramId = program.ProgramId });
			var programOrchestrated = await this.DomainOrchestration(program, programOld);

            if (questionToContinue)
            {
                if (this.Continue(programOrchestrated, programOld) == false)
                    return programOrchestrated;
            }

            return this.SaveWithValidation(programOrchestrated, programOld);
        }

        public override async Task<Program> SavePartial(Program program, bool questionToContinue = false)
        {
            var programOld = await this.GetOne(new ProgramFilter { ProgramId = program.ProgramId });
			var programOrchestrated = await this.DomainOrchestration(program, programOld);

            if (questionToContinue)
            {
                if (this.Continue(programOrchestrated, programOld) == false)
                    return programOrchestrated;
            }

            return SaveWithOutValidation(programOrchestrated, programOld);
        }

        protected override Program SaveWithOutValidation(Program program, Program programOld)
        {
            program = this.SaveDefault(program, programOld);
			this._cacheHelper.ClearCache();

			if (!program.IsValid())
			{
				this._validationResult = program.GetDomainValidation();
				this._validationWarning = program.GetDomainWarning();
				return program;
			}

            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Alterado com sucesso."
            };
            
            return program;
        }

		protected override Program SaveWithValidation(Program program, Program programOld)
        {
            if (!this.IsValid(program))
				return program;
            
            program = this.SaveDefault(program, programOld);
            this._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "Inserido com sucesso."
            };

            this._cacheHelper.ClearCache();
            return program;
        }
		
		protected virtual bool IsValid(Program entity)
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

		protected virtual void Specifications(Program program)
        {
            this._validationResult  = this._validationResult.Merge(new ProgramIsSuitableValidation(this._rep).Validate(program));
			this._validationWarning  = this._validationWarning.Merge(new ProgramIsSuitableWarning(this._rep).Validate(program));
        }

        protected virtual Program SaveDefault(Program program, Program programOld)
        {
			program = this.AuditDefault(program, programOld);

            var isNew = programOld.IsNull();			
            if (isNew)
                program = this.AddDefault(program);
            else
				program = this.UpdateDefault(program);

            return program;
        }
		
        protected virtual Program AddDefault(Program program)
        {
            program = this._rep.Add(program);
            return program;
        }

		protected virtual Program UpdateDefault(Program program)
        {
            program = this._rep.Update(program);
            return program;
        }
				
		public virtual async Task<Program> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Program.ProgramFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<Program> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Program.ProgramFactory().GetDefaultInstance(model, user);
            });
         }
    }
}
