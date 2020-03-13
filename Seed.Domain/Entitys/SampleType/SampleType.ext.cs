using Seed.Domain.Validations;
using System;
using Common.Domain.Model;

namespace Seed.Domain.Entitys
{
    public class SampleType : SampleTypeBase
    {

        public SampleType()
        {

        }

		 public SampleType(int sampletypeid, string name) :
                          base(sampletypeid, name)
                        {

                        }


		public class SampleTypeFactory : SampleTypeFactoryBase
        {
            public SampleType GetDefaultInstance(dynamic data, CurrentUser user)
            {
				return GetDefaultInstanceBase(data, user);
            }
        }

        public bool IsValid()
        {
            base._validationResult = base._validationResult.Merge(new SampleTypeIsConsistentValidation().Validate(this));
            return base._validationResult.IsValid;
        }
        
    }
}
