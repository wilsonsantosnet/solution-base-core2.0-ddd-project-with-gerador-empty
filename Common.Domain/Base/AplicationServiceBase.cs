using Common.Domain.CompositeKey;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Domain.Base
{
    public abstract class ApplicationServiceBase<T, TD, TF>
        where TF : class
        where TD : class
    {
        protected readonly IUnitOfWork _uow;
        protected readonly ICache _cache;
        protected readonly IServiceBase<T, TF> _serviceBase;

        public ApplicationServiceBase(IServiceBase<T, TF> serviceBase, IUnitOfWork uow, ICache cache)
        {
            this._uow = uow;
            this._cache = cache;
            this._serviceBase = serviceBase;
        }
        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public int Commit()
        {
            return _uow.Commit();
        }

        public async Task<int> CommitAsync()
        {
            return await _uow.CommitAsync();
        }

        protected virtual void AddTagCache(string filterKey, string group)
        {
            var tags = this._cache.Get(group) as List<string>;
            if (tags.IsNull()) tags = new List<string>();
            tags.Add(filterKey);
            this._cache.Add(group, tags);
        }

        public virtual async Task<SearchResult<TD>> GetByFilters(FilterBase filters)
        {
            var result = default(SearchResult<TD>);
            result = await this.GetByFiltersWithCache(filters, MapperDomainToResult<TD>);
            return result;
        }

        public virtual async Task<TD> GetOne(FilterBase filters)
        {
            var resultDomain = await this._serviceBase.GetOne(filters as TF);
            var resultDto = this.MapperDomainToDto<TD>(filters, resultDomain);
            return resultDto;
        }

        public virtual async Task<int> Remove(TD entity)
        {
            this.BeginTransaction();

            var model = await this.MapperDtoToDomainForDelete(entity);
            this._serviceBase.Remove(model);

            return await this.CommitAsync();
        }

        public virtual async Task<IEnumerable<TD>> Save(IEnumerable<TD> entitys)
        {
            var entitysChanged = await this.MapperDtoToDomain(entitys);

            this.BeginTransaction();

            var resultDomain = await this._serviceBase.Save(entitysChanged);
            if (!DomainIsValid())
                return this.MapperDomainToDto<TD>(resultDomain);

            await this.CommitAsync();
            return this.MapperDomainToDto<TD>(resultDomain);
        }

        public virtual async Task<IEnumerable<TD>> SavePartial(IEnumerable<TD> entitys)
        {
            this.BeginTransaction();

            var entitysChanged = await this.MapperDtoToDomain(entitys);

            var resultDomain = await this._serviceBase.SavePartial(entitysChanged);
            if (!DomainIsValid())
                return this.MapperDomainToDto<TD>(resultDomain);

            await this.CommitAsync();
            return this.MapperDomainToDto<TD>(resultDomain);
        }

        public virtual async Task<TD> SavePartial(TD entity, bool questionToContinue = false)
        {
            var entityChanged = await this.AlterDomainWithDto(entity);
            if (entityChanged.IsNull())
            {
                this._serviceBase.AddDomainValidation("Não encontrado");
                return entity;
            }

            this.BeginTransaction();

            var resultDomain = await this._serviceBase.SavePartial(entityChanged, questionToContinue);
            if (!DomainIsValid())
                return this.MapperDomainToDto<TD>(resultDomain);

            await this.CommitAsync();

            return this.MapperDomainToDto<TD>(resultDomain);

        }


        public virtual async Task<TD> Save(TD entity, bool questionToContinue = false)
        {
            var model = await this.MapperDtoToDomain(entity);

            this.BeginTransaction();

            var resultDomain = await this._serviceBase.Save(model, questionToContinue);
            if (!DomainIsValid())
                return this.MapperDomainToDto<TD>(resultDomain);

            await this.CommitAsync();
            return this.MapperDomainToDto<TD>(resultDomain);
        }



        private Summary Summary(PaginateResult<T> paginateResult)
        {
            return this._serviceBase.GetSummary(paginateResult);
        }

        private bool DomainIsValid()
        {
            return this._serviceBase.GetDomainValidation().IsValid && this._serviceBase.GetDomainConfirm().IsValid;
        }

        protected virtual async Task<SearchResult<TD>> GetByFiltersWithCache(FilterBase filter, Func<FilterBase, PaginateResult<T>, IEnumerable<TD>> MapperDomainToDto)
        {
            var filterKey = filter.CompositeKey();
            if (filter.ByCache)
                if (this._cache.ExistsKey(filterKey))
                    return (SearchResult<TD>)this._cache.Get(filterKey);

            var paginateResultOptimize = await this._serviceBase.GetByFiltersPaging(filter as TF);
            var result = MapperDomainToDto(filter, paginateResultOptimize);
            var summary = this.Summary(paginateResultOptimize);

            var searchResult = new SearchResult<TD>
            {
                DataList = result,
                Summary = summary,
            };

            if (filter.ByCache)
            {
                if (!searchResult.DataList.IsAny()) return searchResult;
                this._cache.Add(filterKey, searchResult, filter.CacheExpiresTime);
                this.AddTagCache(filterKey, this._serviceBase.GetTagNameCache());
            }

            return searchResult;
        }

        protected async virtual Task<T> MapperDtoToDomain<TDS>(TDS dto) where TDS : class
        {
            return await Task.Run(() =>
            {
                var result = AutoMapper.Mapper.Map<TDS, T>(dto);
                return result;
            });
        }

        protected async virtual Task<T> MapperDtoToDomainForDelete<TDS>(TDS dto) where TDS : class
        {
            return await Task.Run(() =>
            {
                var result = AutoMapper.Mapper.Map<TDS, T>(dto);
                return result;
            });
        }


        protected async virtual Task<IEnumerable<T>> MapperDtoToDomain<TDS>(IEnumerable<TDS> dtos)
        {
            return await Task.Run(() =>
            {
                var result = AutoMapper.Mapper.Map<IEnumerable<TDS>, IEnumerable<T>>(dtos);
                return result;
            });
        }

        protected virtual IEnumerable<TDS> MapperDomainToResult<TDS>(FilterBase filter, PaginateResult<T> dataList)
        {
            var result = filter.IsOnlySummary ? null : AutoMapper.Mapper.Map<IEnumerable<T>, IEnumerable<TDS>>(dataList.ResultPaginatedData);
            return result;
        }

        protected abstract Task<T> AlterDomainWithDto<TDS>(TDS dto) where TDS : class;

        protected virtual IEnumerable<TDS> MapperDomainToDto<TDS>(IEnumerable<T> models)
        {
            return this.MapperDomainToDto<TDS>(new FilterBase(), models);
        }
        protected virtual IEnumerable<TDS> MapperDomainToDto<TDS>(FilterBase filter, IEnumerable<T> models)
        {
            var result = AutoMapper.Mapper.Map<IEnumerable<T>, IEnumerable<TDS>>(models);
            return result;
        }

        protected virtual TDS MapperDomainToDto<TDS>(T model) where TDS : class
        {
            return this.MapperDomainToDto<TDS>(new FilterBase(), model);
        }
        protected virtual TDS MapperDomainToDto<TDS>(FilterBase filter, T model) where TDS : class
        {
            var result = AutoMapper.Mapper.Map<T, TDS>(model);
            return result;
        }

        protected virtual void SetTagNameCache(string _tagNameCache)
        {
            this._serviceBase.SetTagNameCache(_tagNameCache);
        }


        public ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null)
        {
            return this._serviceBase.GetDomainConfirm(filters);
        }

        public WarningSpecificationResult GetDomainWarning(FilterBase filters = null)
        {
            return this._serviceBase.GetDomainWarning(filters);
        }

        public ValidationSpecificationResult GetDomainValidation(FilterBase filters = null)
        {
            return this._serviceBase.GetDomainValidation(filters);
        }


    }

}
