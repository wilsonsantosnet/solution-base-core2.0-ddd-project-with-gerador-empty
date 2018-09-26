using Common.Domain.Base;
using System;

namespace Score.Platform.Account.Domain.Filter
{
    public class ProgramFilterBase : FilterBase
    {

        public virtual int ProgramId { get; set;} 
        public virtual string Description { get; set;} 
        public virtual string Datasource { get; set;} 
        public virtual string DatabaseName { get; set;} 
        public virtual int ThemaId { get; set;} 
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
