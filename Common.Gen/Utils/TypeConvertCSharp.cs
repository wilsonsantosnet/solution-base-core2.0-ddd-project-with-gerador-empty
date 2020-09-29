using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class TypeConvertCSharp
    {

        public static string Convert(string typeSQl, int isNullable)
        {
            switch (typeSQl)
            {
                case "char":
                case "nchar":
                case "nvarchar":
                case "varchar":
                case "text":
                case "ntext":
                    return "string";
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return isNullable == 1 ? "DateTime?" : "DateTime";
                case "bigint":
                    return isNullable == 1 ? "Int64?" : "Int64";
                case "int":
                    return isNullable == 1 ? "int?" : "int";
                case "bit":
                    return isNullable == 1 ? "bool?" : "bool";
                case "tinyint":
                    return isNullable == 1 ? "byte?" : "byte";
                case "smallint":
                case "int16":
                    return isNullable == 1 ? "Int16?" : "Int16";
                case "numeric":
                case "decimal":
                case "money":
                    return isNullable == 1 ? "decimal?" : "decimal";
                case "float":
                    return isNullable == 1 ? "float?" : "float";
                case "image":
                    return isNullable == 1 ? "byte[]" : "byte[]";
                case "uniqueidentifier":
                    return isNullable == 1 ? "Guid?" : "Guid";  

                default:
                    return typeSQl;
            }

        }


        public static int OrderByType(string type)
        {
            switch (type)
            {
                case "string":
                    return 0;

                case "DateTime":
                case "DateTime?":
                case "DateTime2":
                    return 1;

                case "Int64":
                case "Int64?":
                case "Int16":
                case "Int16?":
                case "int?":
                case "int":
                    return 2;
                    
                case "decimal?":
                case "decimal":
                case "float?":
                case "float":
                    return 3;

                case "bool?":
                case "bool":
                    return 4;

                case "byte?":
                case "byte":
                    return 5;

                case "byte[]":
                    return 6;

                default:
                    return 0;
            }

        }


    }
}
