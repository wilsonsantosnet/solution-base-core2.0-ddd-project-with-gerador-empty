using Common.Domain.Base;
using System;

namespace Seed.Domain.Filter
{
    public class SampleFilterBase : FilterBase
    {

        public virtual int SampleId { get; set;} 
        public virtual string Name { get; set;} 
        public virtual string Descricao { get; set;} 
        public virtual int SampleTypeId { get; set;} 
        public virtual bool? Ativo { get; set;} 
        public virtual int? Age { get; set;} 
        public virtual int? Category { get; set;} 
        public virtual DateTime? DatetimeStart { get; set;} 
        public virtual DateTime? DatetimeEnd { get; set;} 
        public virtual DateTime? Datetime { get; set;} 
        public virtual string Tags { get; set;} 
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
