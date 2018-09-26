using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class ProgramIsConsistentValidation : ValidatorSpecification<Program>
    {
        public ProgramIsConsistentValidation()
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Program>(Instance of is consistent specification,"message for user"));
        }

    }
}
