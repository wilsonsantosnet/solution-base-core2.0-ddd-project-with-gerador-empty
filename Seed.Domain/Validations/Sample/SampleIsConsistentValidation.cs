using Common.Validation;
using Seed.Domain.Entitys;
using System;

namespace Seed.Domain.Validations
{
    public class SampleIsConsistentValidation : ValidatorSpecification<Sample>
    {
        public SampleIsConsistentValidation()
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Sample>(Instance of is consistent specification,"message for user"));
        }

    }
}
