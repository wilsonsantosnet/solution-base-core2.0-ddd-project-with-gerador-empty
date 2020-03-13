using Common.Validation;
using Seed.Domain.Entitys;
using Seed.Domain.Interfaces.Repository;
using System;

namespace Seed.Domain.Validations
{
    public class SampleIsSuitableWarning : WarningSpecification<Sample>
    {
        public SampleIsSuitableWarning(ISampleRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Sample>(Instance of suitable warning specification,"message for user"));
        }

    }
}
