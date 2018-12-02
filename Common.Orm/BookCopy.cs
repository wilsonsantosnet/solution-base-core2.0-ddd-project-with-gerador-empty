using Common.Domain.Base;
using Common.Domain.Interfaces;
using FastMember;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Orm
{
    public class BookCopy<T> : IBookCopy<T> where T : class
    {
        private readonly string _connectionString;

        public BookCopy(IOptions<ConfigConnectionStringBase> configSettingsBase)
        {
            this._connectionString = configSettingsBase.Value.Default;
        }

        public async Task<bool> Copy(IEnumerable<T> model, string distinationTableName)
        {
            var propertys = typeof(T).GetProperties();

            var storageParameters = GetStorageParameters(propertys);

            try
            {
                using (var sqlCopy = new SqlBulkCopy(this._connectionString))
                {
                    sqlCopy.DestinationTableName = distinationTableName;
                    using (var reader = ObjectReader.Create(model, storageParameters))
                    {
                        await sqlCopy.WriteToServerAsync(reader);
                    }
                };

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual string[] GetStorageParameters(PropertyInfo[] propertys)
        {
            return propertys.Select(_ => _.Name).ToArray();
        }
    }
}
