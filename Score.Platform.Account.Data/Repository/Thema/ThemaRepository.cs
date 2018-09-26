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
    public class ThemaRepository : Repository<Thema>, IThemaRepository
    {
        private CurrentUser _user;
        public ThemaRepository(DbContextScore ctx, CurrentUser user) : base(ctx)
        {
			this._user = user;
        }

      
        public IQueryable<Thema> GetBySimplefilters(ThemaFilter filters)
        {
            var querybase = this.GetAll(this.DataAgregation(filters))
								.WithBasicFilters(filters)
								.WithCustomFilters(filters)
								.OrderByDomain(filters)
                                .OrderByProperty(filters);
            return querybase;
        }

        public async Task<Thema> GetById(ThemaFilter model)
        {
            var _thema = await this.SingleOrDefaultAsync(this.GetAll(this.DataAgregation(model))
               .Where(_=>_.ThemaId == model.ThemaId));

            return _thema;
        }

		public async Task<IEnumerable<dynamic>> GetDataItem(ThemaFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ThemaId,
				Name = _.Name
            })); 

            return querybase;
        }

        public async Task<IEnumerable<dynamic>> GetDataListCustom(ThemaFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ThemaId

            }));

            return querybase;
        }

		
        public async Task<PaginateResult<dynamic>> GetDataListCustomPaging(ThemaFilter filters)
        {
            var querybase = await this.PagingDataListCustom<dynamic>(filters, this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ThemaId
            }));
            return querybase;
        }

        public async Task<dynamic> GetDataCustom(ThemaFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
               Id = _.ThemaId

            }));

            return querybase;
        }

        protected override dynamic DefineFieldsGetOne(IQueryable<Thema> source, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;
        }

        protected override IQueryable<dynamic> DefineFieldsGetByFilters(IQueryable<Thema> source, FilterBase filters)
        {
			if (filters.QueryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;

        }

        protected override IQueryable<Thema> MapperGetByFiltersToDomainFields(IQueryable<Thema> source, IEnumerable<dynamic> result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return result.Select(_ => new Thema
                {

                }).AsQueryable();
            }

            return result.Select(_ => (Thema)_).AsQueryable();

        }

        protected override Thema MapperGetOneToDomainFields(IQueryable<Thema> source, dynamic result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return new Thema
                {

                };
            }

            return source.SingleOrDefault();
        }

		protected override Expression<Func<Thema, object>>[] DataAgregation(Expression<Func<Thema, object>>[] includes, FilterBase filter)
        {
            return base.DataAgregation(includes, filter);
        }

    }
}
