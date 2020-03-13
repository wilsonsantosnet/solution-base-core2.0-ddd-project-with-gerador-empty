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
    public class SampleTypeRepository : Repository<SampleType>, ISampleTypeRepository
    {
        private CurrentUser _user;
        public SampleTypeRepository(DbContextSeed ctx, CurrentUser user) : base(ctx)
        {
			this._user = user;
        }

      
        public IQueryable<SampleType> GetBySimplefilters(SampleTypeFilter filters)
        {
            var querybase = this.GetAll(this.DataAgregation(filters))
								.WithBasicFilters(filters)
								.WithCustomFilters(filters)
								.OrderByDomain(filters)
                                .OrderByProperty(filters);
            return querybase;
        }

        public async Task<SampleType> GetById(SampleTypeFilter model)
        {
            var _sampletype = await this.SingleOrDefaultAsync(this.GetAll(this.DataAgregation(model))
               .Where(_=>_.SampleTypeId == model.SampleTypeId));

            return _sampletype;
        }

		public async Task<IEnumerable<dynamic>> GetDataItem(SampleTypeFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.SampleTypeId,
				Name = _.Name
            })); 

            return querybase;
        }

        public async Task<IEnumerable<dynamic>> GetDataListCustom(SampleTypeFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.SampleTypeId

            }));

            return querybase;
        }

		
        public async Task<PaginateResult<dynamic>> GetDataListCustomPaging(SampleTypeFilter filters)
        {
            var querybase = await this.PagingDataListCustom<dynamic>(filters, this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.SampleTypeId
            }));
            return querybase;
        }

        public async Task<dynamic> GetDataCustom(SampleTypeFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
               Id = _.SampleTypeId

            }));

            return querybase;
        }

        protected override dynamic DefineFieldsGetOne(IQueryable<SampleType> source, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;
        }

        protected override IQueryable<dynamic> DefineFieldsGetByFilters(IQueryable<SampleType> source, FilterBase filters)
        {
			if (filters.QueryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;

        }

        protected override IQueryable<SampleType> MapperGetByFiltersToDomainFields(IQueryable<SampleType> source, IEnumerable<dynamic> result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return result.Select(_ => new SampleType
                {

                }).AsQueryable();
            }

            return result.Select(_ => (SampleType)_).AsQueryable();

        }

        protected override SampleType MapperGetOneToDomainFields(IQueryable<SampleType> source, dynamic result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return new SampleType
                {

                };
            }

            return source.SingleOrDefault();
        }

		protected override Expression<Func<SampleType, object>>[] DataAgregation(Expression<Func<SampleType, object>>[] includes, FilterBase filter)
        {
            return base.DataAgregation(includes, filter);
        }

    }
}
