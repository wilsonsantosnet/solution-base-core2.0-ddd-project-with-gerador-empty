using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class ThemaIsSuitableValidation : ValidatorSpecification<Thema>
    {
        public ThemaIsSuitableValidation(IThemaRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Thema>(Instance of is suitable,"message for user"));
        }

    }
}
