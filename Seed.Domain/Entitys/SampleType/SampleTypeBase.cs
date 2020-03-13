using Common.Domain.Base;
using Common.Domain.Model;
using System;

namespace Seed.Domain.Entitys
{
    public class SampleTypeBase : DomainBase
    {
        public SampleTypeBase()
        {

        }

		public SampleTypeBase(int sampletypeid, string name) 
        {
            this.SampleTypeId = sampletypeid;
            this.Name = name;

        }


		public class SampleTypeFactoryBase
        {
            public virtual SampleType GetDefaultInstanceBase(dynamic data, CurrentUser user)
            {
                var construction = new SampleType(data.SampleTypeId,
                                        data.Name);



				construction.SetConfirmBehavior(data.ConfirmBehavior);
				construction.SetAttributeBehavior(data.AttributeBehavior);
        		return construction;
            }

        }

        public virtual int SampleTypeId { get; protected set; }
        public virtual string Name { get; protected set; }



    }
}
