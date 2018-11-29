using System.Linq;
using System.Reflection;
using Common.Domain.Base;
using Microsoft.Extensions.Options;

namespace Common.Orm
{
    public class BookCopyCustom<T> : BookCopy<T> where T : class
    {
        public BookCopyCustom(IOptions<Connectionstring> ConfigConnectionStringBase) : base(configSettingsBase)
        {

        }

        protected override string[] GetStorageParameters(PropertyInfo[] propertys)
        {
            return propertys.Select(_ => _.Name)
                           .Where(_ => _ != "AttributeBehavior")
                           .Where(_ => _ != "ConfirmBehavior")
                           .ToArray();
        }

    }
}
