using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class TenantIsSuitableWarning : WarningSpecification<Tenant>
    {
        public TenantIsSuitableWarning(ITenantRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Tenant>(Instance of suitable warning specification,"message for user"));
        }

    }
}
