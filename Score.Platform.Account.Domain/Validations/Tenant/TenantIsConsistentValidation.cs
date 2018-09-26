using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class TenantIsConsistentValidation : ValidatorSpecification<Tenant>
    {
        public TenantIsConsistentValidation()
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Tenant>(Instance of is consistent specification,"message for user"));
        }

    }
}
