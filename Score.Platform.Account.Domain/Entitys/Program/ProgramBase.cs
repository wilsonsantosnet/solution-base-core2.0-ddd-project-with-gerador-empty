using Common.Domain.Base;
using Common.Domain.Model;
using System;

namespace Score.Platform.Account.Domain.Entitys
{
    public class ProgramBase : DomainBaseWithUserCreate
    {
        public ProgramBase()
        {

        }

		public ProgramBase(int programid, string description, string datasource, string databasename, int themaid) 
        {
            this.ProgramId = programid;
            this.Description = description;
            this.Datasource = datasource;
            this.DatabaseName = databasename;
            this.ThemaId = themaid;

        }


		public class ProgramFactoryBase
        {
            public virtual Program GetDefaultInstanceBase(dynamic data, CurrentUser user)
            {
                var construction = new Program(data.ProgramId,
                                        data.Description,
                                        data.Datasource,
                                        data.DatabaseName,
                                        data.ThemaId);



				construction.SetConfirmBehavior(data.ConfirmBehavior);
				construction.SetAttributeBehavior(data.AttributeBehavior);
        		return construction;
            }

        }

        public virtual int ProgramId { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string Datasource { get; protected set; }
        public virtual string DatabaseName { get; protected set; }
        public virtual int ThemaId { get; protected set; }



    }
}
