import { Injectable } from '@angular/core';


@Injectable()
export class LocalStorageService {


    export() {

        return {
            Export: _export,
        }

        function _export(data : any, nome : any) {


            var xml;
            xml = '<?xml version="1.0"?><ss:Workbook xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">' +
                '<ss:Styles><ss:Style ss:ID="1"><ss:Font ss:Bold="1"/></ss:Style></ss:Styles>';

            xml += '<ss:Worksheet ss:Name="' + nome + '">';
            xml += ' <ss:Table>';

            xml += '  <ss:Row ss:StyleID="1">';
            for (var propriedade in data[0]) {
                xml += '<ss:Cell><ss:Data ss:Type="String">' + propriedade + '</ss:Data></ss:Cell>';
            };
            xml += '  </ss:Row>';

            for (var i = 0; i < data.length; i++) {
                var intancia = data[i];
                xml += ' <ss:Row>';
                for (var propriedade in intancia) {
                    var valor = intancia[propriedade];
                    var ehNumber = /^\d+$/.test(valor);
                    xml += '<ss:Cell><ss:Data ss:Type="' + ((ehNumber) ? 'Number' : 'String') + '">' + valor + '</ss:Data></ss:Cell>';
                }
                xml += ' </ss:Row>';
            }

            xml += ' </ss:Table>';
            xml += '</ss:Worksheet>';
            xml += '</ss:Workbook>';


            //var fileName = nome + '_' + moment().format('YYYYMMDDHHmmss') + '.xls';
            //var blob = new Blob([xml], { type: "application/vnd.ms-excel" })
            //window.saveAs(blob, fileName);

        }
    }
}
