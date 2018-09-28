using System;

namespace Common.Domain.Base
{
    public class DomainBaseWithUserCreate : DomainBase
    {
        public int UserCreateId { get; protected set; }
        public DateTime UserCreateDate { get; protected set; }
        public int? UserAlterId { get; protected set; }
        public DateTime? UserAlterDate { get; protected set; }

        public virtual void SetUserCreate(int userCreateId, DateTime userCreateDate)
        {
            this.UserCreateId = userCreateId;
            this.UserCreateDate = userCreateDate;
        }

        public virtual void SetUserCreate(int userCreateId)
        {
            this.UserCreateId = userCreateId;
            this.UserCreateDate = DateTime.Now.ToTimeZone();
        }

        public virtual void SetUserUpdate(int userAlterId)
        {
            this.UserAlterId = userAlterId;
            this.UserAlterDate = DateTime.Now.ToTimeZone();
        }

    }
}
