using Common.Validation;
using Seed.Domain.Entitys;
using Seed.Domain.Interfaces.Repository;
using System;

namespace Seed.Domain.Validations
{
    public class SampleTypeIsSuitableWarning : WarningSpecification<SampleType>
    {
        public SampleTypeIsSuitableWarning(ISampleTypeRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<SampleType>(Instance of suitable warning specification,"message for user"));
        }

    }
}
