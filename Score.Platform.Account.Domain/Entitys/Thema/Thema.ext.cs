using Score.Platform.Account.Domain.Validations;
using System;
using Common.Domain.Model;

namespace Score.Platform.Account.Domain.Entitys
{
    public class Thema : ThemaBase
    {

        public Thema()
        {

        }

		 public Thema(int themaid, string name, string description) :
                          base(themaid, name, description)
                        {

                        }


		public class ThemaFactory : ThemaFactoryBase
        {
            public Thema GetDefaultInstance(dynamic data, CurrentUser user)
            {
				return GetDefaultInstanceBase(data, user);
            }
        }

        public bool IsValid()
        {
            base._validationResult = base._validationResult.Merge(new ThemaIsConsistentValidation().Validate(this));
            return base._validationResult.IsValid;
        }
        
    }
}
