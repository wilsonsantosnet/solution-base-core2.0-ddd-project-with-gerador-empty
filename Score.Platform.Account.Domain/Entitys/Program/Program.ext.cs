using Score.Platform.Account.Domain.Validations;
using System;
using Common.Domain.Model;

namespace Score.Platform.Account.Domain.Entitys
{
    public class Program : ProgramBase
    {

        public Program()
        {

        }

        public Program(int programid, string description, string datasource, string databasename, int themaid) : base(programid, description, datasource, databasename, themaid)
        {

        }

        public class ProgramFactory : ProgramFactoryBase
        {
            public Program GetDefaultInstance(dynamic data, CurrentUser user)
            {
                return GetDefaultInstanceBase(data, user);
            }
        }

        public bool IsValid()
        {
            base._validationResult = base._validationResult.Merge(new ProgramIsConsistentValidation().Validate(this));
            return base._validationResult.IsValid;
        }

    }
}
