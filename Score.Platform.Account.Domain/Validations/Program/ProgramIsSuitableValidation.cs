using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class ProgramIsSuitableValidation : ValidatorSpecification<Program>
    {
        public ProgramIsSuitableValidation(IProgramRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Program>(Instance of is suitable,"message for user"));
        }

    }
}
