using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class ThemaIsConsistentValidation : ValidatorSpecification<Thema>
    {
        public ThemaIsConsistentValidation()
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Thema>(Instance of is consistent specification,"message for user"));
        }

    }
}
