using Common.Domain.Base;
using Common.Orm;
using Score.Platform.Account.Data.Context;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Common.Domain.Model;

namespace Score.Platform.Account.Data.Repository
{
    public class ProgramRepository : Repository<Program>, IProgramRepository
    {
        private CurrentUser _user;
        public ProgramRepository(DbContextScore ctx, CurrentUser user) : base(ctx)
        {
			this._user = user;
        }

      
        public IQueryable<Program> GetBySimplefilters(ProgramFilter filters)
        {
            var querybase = this.GetAll(this.DataAgregation(filters))
								.WithBasicFilters(filters)
								.WithCustomFilters(filters)
								.OrderByDomain(filters)
                                .OrderByProperty(filters);
            return querybase;
        }

        public async Task<Program> GetById(ProgramFilter model)
        {
            var _program = await this.SingleOrDefaultAsync(this.GetAll(this.DataAgregation(model))
               .Where(_=>_.ProgramId == model.ProgramId));

            return _program;
        }

		public async Task<IEnumerable<dynamic>> GetDataItem(ProgramFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ProgramId,
				Name = _.Description
            })); 

            return querybase;
        }

        public async Task<IEnumerable<dynamic>> GetDataListCustom(ProgramFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ProgramId

            }));

            return querybase;
        }

		
        public async Task<PaginateResult<dynamic>> GetDataListCustomPaging(ProgramFilter filters)
        {
            var querybase = await this.PagingDataListCustom<dynamic>(filters, this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ProgramId
            }));
            return querybase;
        }

        public async Task<dynamic> GetDataCustom(ProgramFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
               Id = _.ProgramId

            }));

            return querybase;
        }

        protected override dynamic DefineFieldsGetOne(IQueryable<Program> source, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;
        }

        protected override IQueryable<dynamic> DefineFieldsGetByFilters(IQueryable<Program> source, FilterBase filters)
        {
			if (filters.QueryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;

        }

        protected override IQueryable<Program> MapperGetByFiltersToDomainFields(IQueryable<Program> source, IEnumerable<dynamic> result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return result.Select(_ => new Program
                {

                }).AsQueryable();
            }

            return result.Select(_ => (Program)_).AsQueryable();

        }

        protected override Program MapperGetOneToDomainFields(IQueryable<Program> source, dynamic result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return new Program
                {

                };
            }

            return source.SingleOrDefault();
        }

		protected override Expression<Func<Program, object>>[] DataAgregation(Expression<Func<Program, object>>[] includes, FilterBase filter)
        {
            return base.DataAgregation(includes, filter);
        }

    }
}
