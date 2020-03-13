using Common.Validation;
using Seed.Domain.Entitys;
using Seed.Domain.Interfaces.Repository;
using System;

namespace Seed.Domain.Validations
{
    public class SampleIsSuitableValidation : ValidatorSpecification<Sample>
    {
        public SampleIsSuitableValidation(ISampleRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Sample>(Instance of is suitable,"message for user"));
        }

    }
}
