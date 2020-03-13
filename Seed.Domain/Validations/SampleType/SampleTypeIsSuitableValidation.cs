using Common.Validation;
using Seed.Domain.Entitys;
using Seed.Domain.Interfaces.Repository;
using System;

namespace Seed.Domain.Validations
{
    public class SampleTypeIsSuitableValidation : ValidatorSpecification<SampleType>
    {
        public SampleTypeIsSuitableValidation(ISampleTypeRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<SampleType>(Instance of is suitable,"message for user"));
        }

    }
}
