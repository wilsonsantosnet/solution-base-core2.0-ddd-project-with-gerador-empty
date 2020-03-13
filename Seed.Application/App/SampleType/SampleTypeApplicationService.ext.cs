using Common.Domain.Interfaces;
using Seed.Application.Interfaces;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using Seed.Domain.Interfaces.Services;
using Seed.Dto;
using System.Linq;
using System.Collections.Generic;
using Common.Domain.Base;
using Common.Domain.Model;
using AutoMapper;

namespace Seed.Application
{
    public class SampleTypeApplicationService : SampleTypeApplicationServiceBase
    {

        public SampleTypeApplicationService(ISampleTypeService service, IUnitOfWork uow, ICache cache, CurrentUser user, IMapper mapper) :
            base(service, uow, cache, user, mapper)
        {
  
        }

        protected override System.Collections.Generic.IEnumerable<TDS> MapperDomainToResult<TDS>(FilterBase filter, PaginateResult<SampleType> dataList)
        {
            return base.MapperDomainToResult<SampleTypeDtoSpecializedResult>(filter, dataList) as IEnumerable<TDS>;
        }


    }
}
