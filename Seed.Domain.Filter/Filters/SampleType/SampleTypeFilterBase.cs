using Common.Domain.Base;
using System;

namespace Seed.Domain.Filter
{
    public class SampleTypeFilterBase : FilterBase
    {

        public virtual int SampleTypeId { get; set;} 
        public virtual string Name { get; set;} 


    }
}
