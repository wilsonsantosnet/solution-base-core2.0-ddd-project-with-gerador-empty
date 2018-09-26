using Common.Domain.Base;
using Common.Domain.Model;
using System;

namespace Score.Platform.Account.Domain.Entitys
{
    public class ThemaBase : DomainBase
    {
        public ThemaBase()
        {

        }

		public ThemaBase(int themaid, string name, string description) 
        {
            this.ThemaId = themaid;
            this.Name = name;
            this.Description = description;

        }


		public class ThemaFactoryBase
        {
            public virtual Thema GetDefaultInstanceBase(dynamic data, CurrentUser user)
            {
                var construction = new Thema(data.ThemaId,
                                        data.Name,
                                        data.Description);



				construction.SetConfirmBehavior(data.ConfirmBehavior);
				construction.SetAttributeBehavior(data.AttributeBehavior);
        		return construction;
            }

        }

        public virtual int ThemaId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }



    }
}
