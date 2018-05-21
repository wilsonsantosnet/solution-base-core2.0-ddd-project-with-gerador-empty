using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Common.Domain.Base;
using System.IO;
using OfficeOpenXml;

namespace Common.API
{
    public class ExportExcel<T>
    {
        private FilterBase _filter;
        private string _fileName;

        public ExportExcel(FilterBase filter)
        {
            this._filter = filter;
        }


        public virtual string GetFileName()
        {
            return this._fileName;
        }

        public virtual string ContentTypeExcel()
        {
            return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
        public virtual byte[] ExportFile(HttpResponse response, IEnumerable<T> data, string nome)
        {
            var content = ExportByXml(data, nome);
            var fileName = Guid.NewGuid().ToString() + ".xls";
            response.Headers.Add("content-disposition", "attachment; filename=Information" + fileName);
            response.ContentType = this.ContentTypeExcel();
            this._fileName = fileName;
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
        public virtual byte[] ExportFile(HttpResponse response, IEnumerable<T> data, string nome, string rootPath)
        {
            var fileName = Guid.NewGuid().ToString() + ".xlsx";
            response.Headers.Add("content-disposition", "attachment; filename=Information" + fileName);
            response.ContentType = this.ContentTypeExcel();
            this._fileName = fileName;
            var path = Path.Combine(rootPath, fileName);
            var content = this.ExportByComponent(data, nome, new FileInfo(path));
            return content;

        }

        public virtual string ExportByXml(IEnumerable<T> data, string nome)
        {

            var xml = string.Empty;

            xml = "<?xml version=\"1.0\"?><ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">" +
                "<ss:Styles><ss:Style ss:ID=\"1\"><ss:Font ss:Bold=\"1\"/></ss:Style></ss:Styles>";

            xml += "<ss:Worksheet ss:Name=\"" + nome + "\">";
            xml += " <ss:Table>";

            xml += "  <ss:Row ss:StyleID=\"1\">";

            foreach (var item in data.FirstOrDefault().GetType().GetTypeInfo().GetProperties()
                .Where(_ => _.GetType().GetTypeInfo().IsClass))
            {
                var propriedade = item.Name;
                xml += "<ss:Cell><ss:Data ss:Type=\"String\">" + propriedade + "</ss:Data></ss:Cell>";
            };
            xml += "  </ss:Row>";

            foreach (var item in data)
            {
                var intancia = item;
                xml += " <ss:Row>";
                foreach (var subItem in item.GetType().GetTypeInfo().GetProperties()
                    .Where(_ => _.GetType().GetTypeInfo().IsClass))
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

        public virtual byte[] ExportByComponent(IEnumerable<T> data, string nome, FileInfo file)
        {
            var content = default(byte[]);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets.Add(nome);
                var columnIndex = 1;
                var rowIndex = 1;
                foreach (var item in data.FirstOrDefault().GetType().GetTypeInfo().GetProperties()
                .Where(_ => _.GetType().GetTypeInfo().IsClass))
                {
                    var propriedade = item.Name;
                    worksheet.Cells[rowIndex, columnIndex].Value = propriedade;

                    columnIndex++;
                };


                rowIndex = 2;
                foreach (var item in data)
                {
                    var intancia = item;
                    columnIndex = 1;
                    foreach (var subItem in item.GetType().GetTypeInfo().GetProperties()
                        .Where(_ => _.GetType().GetTypeInfo().IsClass))
                    {
                        var valor = subItem.GetValue(item);
                        if (valor != null)
                        {
                            var isDate = valor.IsDate();
                            if (isDate)
                                worksheet.Cells[rowIndex, columnIndex].Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                        }

                        worksheet.Cells[rowIndex, columnIndex].Value = valor;
                        columnIndex++;
                    }
                    rowIndex++;
                }

                content = package.GetAsByteArray();
            }

            return content;

        }

    }
}
