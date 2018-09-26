using Common.Domain.Base;
using System;

namespace Score.Platform.Account.Domain.Filter
{
    public class ThemaFilterBase : FilterBase
    {

        public virtual int ThemaId { get; set;} 
        public virtual string Name { get; set;} 
        public virtual string Description { get; set;} 


    }
}
