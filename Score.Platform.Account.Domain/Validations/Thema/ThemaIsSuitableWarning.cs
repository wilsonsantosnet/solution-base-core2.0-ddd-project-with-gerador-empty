using Common.Validation;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;

namespace Score.Platform.Account.Domain.Validations
{
    public class ThemaIsSuitableWarning : WarningSpecification<Thema>
    {
        public ThemaIsSuitableWarning(IThemaRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Thema>(Instance of suitable warning specification,"message for user"));
        }

    }
}
