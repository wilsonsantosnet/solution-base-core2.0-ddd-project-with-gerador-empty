using System.Collections.Generic;

namespace Common.Domain.Interfaces
{
    public interface IImportExcel
    {
        IEnumerable<dynamic> Import(string filePath);
        IEnumerable<Dictionary<string, object>> ImportExcelToDictoray(string filePath);
    }
}