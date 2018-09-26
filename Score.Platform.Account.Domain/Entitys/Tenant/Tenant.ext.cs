using Score.Platform.Account.Domain.Validations;
using System;
using Common.Domain.Model;

namespace Score.Platform.Account.Domain.Entitys
{
    public class Tenant : TenantBase
    {

        public virtual Program Program { get; protected set; }

        public Tenant()
        {

        }

        public Tenant(int tenantid, string name, string email, string password, bool active, int programid, bool changepasswordnextlogin) :
                         base(tenantid, name, email, password, active, programid, changepasswordnextlogin)
        {

        }


        public class TenantFactory : TenantFactoryBase
        {
            public Tenant GetDefaultInstance(dynamic data, CurrentUser user)
            {
                return GetDefaultInstanceBase(data, user);
            }
        }

        public bool IsValid()
        {
            base._validationResult = base._validationResult.Merge(new TenantIsConsistentValidation().Validate(this));
            return base._validationResult.IsValid;
        }

    }
}
