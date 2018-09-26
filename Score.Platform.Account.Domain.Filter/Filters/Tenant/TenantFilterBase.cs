using Common.Domain.Base;
using System;

namespace Score.Platform.Account.Domain.Filter
{
    public class TenantFilterBase : FilterBase
    {

        public virtual int TenantId { get; set;} 
        public virtual string Name { get; set;} 
        public virtual string Email { get; set;} 
        public virtual string Password { get; set;} 
        public virtual bool? Active { get; set;} 
        public virtual int ProgramId { get; set;} 
        public virtual Guid? GuidResetPassword { get; set;} 
        public virtual DateTime? DateResetPasswordStart { get; set;} 
        public virtual DateTime? DateResetPasswordEnd { get; set;} 
        public virtual DateTime? DateResetPassword { get; set;} 
        public virtual bool? ChangePasswordNextLogin { get; set;} 
        public virtual int UserCreateId { get; set;} 
        public virtual DateTime UserCreateDateStart { get; set;} 
        public virtual DateTime UserCreateDateEnd { get; set;} 
        public virtual DateTime UserCreateDate { get; set;} 
        public virtual int? UserAlterId { get; set;} 
        public virtual DateTime? UserAlterDateStart { get; set;} 
        public virtual DateTime? UserAlterDateEnd { get; set;} 
        public virtual DateTime? UserAlterDate { get; set;} 


    }
}
