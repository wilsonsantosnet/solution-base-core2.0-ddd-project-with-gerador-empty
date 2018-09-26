using Common.API;
using Common.Domain.Base;

namespace Score.Platform.Account.CrossCuting
{
    public class ExportExcelCustom<T> : ExportExcel<T>
    {
        public ExportExcelCustom(FilterBase filter) : base(filter)
        {



        }
    }
}
