using Common.Domain.Interfaces;
using Score.Platform.Account.Application.Interfaces;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using Score.Platform.Account.Domain.Interfaces.Services;
using Score.Platform.Account.Dto;
using System.Linq;
using System.Collections.Generic;
using Common.Domain.Base;
using Common.Domain.Model;

namespace Score.Platform.Account.Application
{
    public class ThemaApplicationService : ThemaApplicationServiceBase
    {

        public ThemaApplicationService(IThemaService service, IUnitOfWork uow, ICache cache, CurrentUser user) :
            base(service, uow, cache, user)
        {
  
        }

        protected override System.Collections.Generic.IEnumerable<TDS> MapperDomainToResult<TDS>(FilterBase filter, PaginateResult<Thema> dataList)
        {
            return base.MapperDomainToResult<ThemaDtoSpecializedResult>(filter, dataList) as IEnumerable<TDS>;
        }


    }
}
