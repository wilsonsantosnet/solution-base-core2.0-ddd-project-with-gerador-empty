using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class TenantIsSuitableValidation : ValidatorSpecification<Tenant>
    {
        public TenantIsSuitableValidation(ITenantRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Tenant>(Instance of is suitable,"message for user"));
        }

    }
}
