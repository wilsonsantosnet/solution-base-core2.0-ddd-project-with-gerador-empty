using Common.API;
using Common.Domain.Base;

namespace Seed.CrossCuting
{
    public class ExportExcelCustom<T> : ExportExcel<T>
    {
        public ExportExcelCustom(FilterBase filter) : base(filter)
        {



        }
    }
}
