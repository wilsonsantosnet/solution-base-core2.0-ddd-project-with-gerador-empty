using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Gen
{
    class HelperExcel<T>
    {

        private string _fileName;

        public virtual string GetFileName()
        {
            return this._fileName;
        }

        public virtual string ContentTypeExcel()
        {
            return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }

        public virtual byte[] ExportFile(IEnumerable<T> data, string nome)
        {
            var content = ExportByXml(data, nome);
            var fileName = Guid.NewGuid().ToString() + ".xls";
            //response.Headers.Add("content-disposition", "attachment; filename=Information" + fileName);
            //response.ContentType = this.ContentTypeExcel();
            this._fileName = fileName;
            return Encoding.UTF8.GetBytes(content);
        }

        private string ExportByXml(IEnumerable<T> data, string nome)
        {

            var xml = string.Empty;

            xml = "<?xml version=\"1.0\"?><ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">" +
                "<ss:Styles><ss:Style ss:ID=\"1\"><ss:Font ss:Bold=\"1\"/></ss:Style></ss:Styles>";

            xml += "<ss:Worksheet ss:Name=\"" + nome + "\">";
            xml += " <ss:Table>";

            xml += "  <ss:Row ss:StyleID=\"1\">";

            foreach (var item in data.FirstOrDefault().GetType().GetProperties()
                .Where(_ => _.GetType().IsClass))
            {
                var propriedade = item.Name;
                xml += "<ss:Cell><ss:Data ss:Type=\"String\">" + propriedade + "</ss:Data></ss:Cell>";
            };
            xml += "  </ss:Row>";

            foreach (var item in data)
            {
                var intancia = item;
                xml += " <ss:Row>";
                foreach (var subItem in item.GetType().GetProperties()
                    .Where(_ => _.GetType().IsClass))
                {
                    var valor = subItem.GetValue(item);
                    if (valor.IsNotNull())
                    {
                        var ehNumber = new Regex("/^\\d +$/").IsMatch(valor.ToString());
                        xml += "<ss:Cell><ss:Data ss:Type=\"" + ((ehNumber) ? "Number" : "String") + "\">" + valor + "</ss:Data></ss:Cell>";
                    }
                    else xml += "<ss:Cell><ss:Data ss:Type=\"String\"></ss:Data></ss:Cell>";
                }
                xml += " </ss:Row>";
            }

            xml += "</ss:Table>";
            xml += "</ss:Worksheet>";
            xml += "</ss:Workbook>";

            return xml;
        }
    }
}
