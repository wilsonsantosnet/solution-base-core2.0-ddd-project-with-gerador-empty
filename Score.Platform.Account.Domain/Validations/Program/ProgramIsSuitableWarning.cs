using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class ProgramIsSuitableWarning : WarningSpecification<Program>
    {
        public ProgramIsSuitableWarning(IProgramRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Program>(Instance of suitable warning specification,"message for user"));
        }

    }
}
