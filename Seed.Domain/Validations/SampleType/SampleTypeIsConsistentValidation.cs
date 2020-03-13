using Common.Validation;
using Seed.Domain.Entitys;
using System;

namespace Seed.Domain.Validations
{
    public class SampleTypeIsConsistentValidation : ValidatorSpecification<SampleType>
    {
        public SampleTypeIsConsistentValidation()
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<SampleType>(Instance of is consistent specification,"message for user"));
        }

    }
}
