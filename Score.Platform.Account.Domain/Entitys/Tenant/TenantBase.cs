using Common.Domain.Base;
using Common.Domain.Model;
using System;

namespace Score.Platform.Account.Domain.Entitys
{
    public class TenantBase : DomainBaseWithUserCreate
    {
        public TenantBase()
        {

        }

		public TenantBase(int tenantid, string name, string email, string password, bool active, int programid, bool changepasswordnextlogin) 
        {
            this.TenantId = tenantid;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Active = active;
            this.ProgramId = programid;
            this.ChangePasswordNextLogin = changepasswordnextlogin;

        }


		public class TenantFactoryBase
        {
            public virtual Tenant GetDefaultInstanceBase(dynamic data, CurrentUser user)
            {
                var construction = new Tenant(data.TenantId,
                                        data.Name,
                                        data.Email,
                                        data.Password,
                                        data.Active,
                                        data.ProgramId,
                                        data.ChangePasswordNextLogin);

                construction.SetarGuidResetPassword(data.GuidResetPassword);
                construction.SetarDateResetPassword(data.DateResetPassword);


				construction.SetConfirmBehavior(data.ConfirmBehavior);
				construction.SetAttributeBehavior(data.AttributeBehavior);
        		return construction;
            }

        }

        public virtual int TenantId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual int ProgramId { get; protected set; }
        public virtual Guid? GuidResetPassword { get; protected set; }
        public virtual DateTime? DateResetPassword { get; protected set; }
        public virtual bool ChangePasswordNextLogin { get; protected set; }

		public virtual void SetarGuidResetPassword(Guid? guidresetpassword)
		{
			this.GuidResetPassword = guidresetpassword;
		}
		public virtual void SetarDateResetPassword(DateTime? dateresetpassword)
		{
			this.DateResetPassword = dateresetpassword;
		}


    }
}
