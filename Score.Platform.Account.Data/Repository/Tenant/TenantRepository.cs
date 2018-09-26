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
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        private CurrentUser _user;
        public TenantRepository(DbContextScore ctx, CurrentUser user) : base(ctx)
        {
			this._user = user;
        }

      
        public IQueryable<Tenant> GetBySimplefilters(TenantFilter filters)
        {
            var querybase = this.GetAll(this.DataAgregation(filters))
								.WithBasicFilters(filters)
								.WithCustomFilters(filters)
								.OrderByDomain(filters)
                                .OrderByProperty(filters);
            return querybase;
        }

        public async Task<Tenant> GetById(TenantFilter model)
        {
            var _tenant = await this.SingleOrDefaultAsync(this.GetAll(this.DataAgregation(model))
               .Where(_=>_.TenantId == model.TenantId));

            return _tenant;
        }

		public async Task<IEnumerable<dynamic>> GetDataItem(TenantFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.TenantId,
				Name = _.Name
            })); 

            return querybase;
        }

        public async Task<IEnumerable<dynamic>> GetDataListCustom(TenantFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.TenantId

            }));

            return querybase;
        }

		
        public async Task<PaginateResult<dynamic>> GetDataListCustomPaging(TenantFilter filters)
        {
            var querybase = await this.PagingDataListCustom<dynamic>(filters, this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.TenantId
            }));
            return querybase;
        }

        public async Task<dynamic> GetDataCustom(TenantFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
               Id = _.TenantId

            }));

            return querybase;
        }

        protected override dynamic DefineFieldsGetOne(IQueryable<Tenant> source, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;
        }

        protected override IQueryable<dynamic> DefineFieldsGetByFilters(IQueryable<Tenant> source, FilterBase filters)
        {
			if (filters.QueryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;

        }

        protected override IQueryable<Tenant> MapperGetByFiltersToDomainFields(IQueryable<Tenant> source, IEnumerable<dynamic> result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return result.Select(_ => new Tenant
                {

                }).AsQueryable();
            }

            return result.Select(_ => (Tenant)_).AsQueryable();

        }

        protected override Tenant MapperGetOneToDomainFields(IQueryable<Tenant> source, dynamic result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return new Tenant
                {

                };
            }

            return source.SingleOrDefault();
        }

		protected override Expression<Func<Tenant, object>>[] DataAgregation(Expression<Func<Tenant, object>>[] includes, FilterBase filter)
        {
            return base.DataAgregation(includes, filter);
        }

    }
}
