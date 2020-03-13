using Common.Domain.Base;
using Common.Orm;
using Seed.Data.Context;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using Seed.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Common.Domain.Model;

namespace Seed.Data.Repository
{
    public class SampleRepository : Repository<Sample>, ISampleRepository
    {
        private CurrentUser _user;
        public SampleRepository(DbContextSeed ctx, CurrentUser user) : base(ctx)
        {
			this._user = user;
        }

      
        public IQueryable<Sample> GetBySimplefilters(SampleFilter filters)
        {
            var querybase = this.GetAll(this.DataAgregation(filters))
								.WithBasicFilters(filters)
								.WithCustomFilters(filters)
								.OrderByDomain(filters)
                                .OrderByProperty(filters);
            return querybase;
        }

        public async Task<Sample> GetById(SampleFilter model)
        {
            var _sample = await this.SingleOrDefaultAsync(this.GetAll(this.DataAgregation(model))
               .Where(_=>_.SampleId == model.SampleId));

            return _sample;
        }

		public async Task<IEnumerable<dynamic>> GetDataItem(SampleFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.SampleId,
				Name = _.Name
            })); 

            return querybase;
        }

        public async Task<IEnumerable<dynamic>> GetDataListCustom(SampleFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.SampleId

            }));

            return querybase;
        }

		
        public async Task<PaginateResult<dynamic>> GetDataListCustomPaging(SampleFilter filters)
        {
            var querybase = await this.PagingDataListCustom<dynamic>(filters, this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.SampleId
            }));
            return querybase;
        }

        public async Task<dynamic> GetDataCustom(SampleFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
               Id = _.SampleId

            }));

            return querybase;
        }

        protected override dynamic DefineFieldsGetOne(IQueryable<Sample> source, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;
        }

        protected override IQueryable<dynamic> DefineFieldsGetByFilters(IQueryable<Sample> source, FilterBase filters)
        {
			if (filters.QueryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;

        }

        protected override IQueryable<Sample> MapperGetByFiltersToDomainFields(IQueryable<Sample> source, IEnumerable<dynamic> result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return result.Select(_ => new Sample
                {

                }).AsQueryable();
            }

            return result.Select(_ => (Sample)_).AsQueryable();

        }

        protected override Sample MapperGetOneToDomainFields(IQueryable<Sample> source, dynamic result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return new Sample
                {

                };
            }

            return source.SingleOrDefault();
        }

		protected override Expression<Func<Sample, object>>[] DataAgregation(Expression<Func<Sample, object>>[] includes, FilterBase filter)
        {
            return base.DataAgregation(includes, filter);
        }

    }
}
