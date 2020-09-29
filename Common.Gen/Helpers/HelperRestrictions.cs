using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class HelperRestrictions
    {


        public static bool RunRestrictions(FieldConfigShow rest, TableInfo tableInfo, Info info, string propertyName, EOperation opList)
        {
            switch (rest)
            {
                case FieldConfigShow.ShowAll:
                    return RestrictionBlock(tableInfo, info, propertyName, opList);

                case FieldConfigShow.HideAll:
                    return RestrictionAllow(tableInfo, info, propertyName, opList);

            }
            return false;
        }

        private static bool RestrictionAllow(TableInfo tableInfo, Info info, string propertyName, EOperation opList)
        {
            if (opList == EOperation.Front_angular_FieldsCreate || opList == EOperation.Front_angular_Service)
                return FieldVerification(tableInfo, info, propertyName, "Create", "True");

            if (opList == EOperation.Front_angular_FieldsEdit)
                return FieldVerification(tableInfo, info, propertyName, "Edit", "True");

            if (opList == EOperation.Front_angular_Grid || opList == EOperation.Front_angular_Component)
                return FieldVerification(tableInfo, info, propertyName, "List", "True");

            if (opList == EOperation.Front_angular_FieldFilter)
                return FieldVerification(tableInfo, info, propertyName, "Filter", "True");

            if (opList == EOperation.Front_angular_FieldDetails)
                return FieldVerification(tableInfo, info, propertyName, "Details", "True");
            
            if (opList == EOperation.Front_angular_FieldGrid)
                return FieldVerification(tableInfo, info, propertyName, "List", "True");

            return false;
        }

        private static bool RestrictionBlock(TableInfo tableInfo, Info info, string propertyName, EOperation opList)
        {
            if (opList == EOperation.Front_angular_FieldsCreate)
                return FieldVerification(tableInfo, info, propertyName, "Create", "False");

            if (opList == EOperation.Front_angular_FieldsEdit)
                return FieldVerification(tableInfo, info, propertyName, "Edit", "False");

            if (opList == EOperation.Front_angular_Grid || opList == EOperation.Front_angular_Component)
                return FieldVerification(tableInfo, info, propertyName, "List", "False");

            if (opList == EOperation.Front_angular_FieldFilter)
                return FieldVerification(tableInfo, info, propertyName, "Filter", "False");

            if (opList == EOperation.Front_angular_FieldDetails)
                return FieldVerification(tableInfo, info, propertyName, "Details", "False");

            if (opList == EOperation.Front_angular_FieldGrid)
                return FieldVerification(tableInfo, info, propertyName, "List", "False");


            return false;
        }

        public static bool FieldVerification(TableInfo tableInfo, Info info, string propertyName, string propertyVerification, string conditionProperty)
        {
            if (tableInfo.FieldsConfigShow == FieldConfigShow.ShowAll && tableInfo.FieldsConfig.IsNull())
                return false;

            if (tableInfo.FieldsConfigShow == FieldConfigShow.HideAll && tableInfo.FieldsConfig.IsNull())
                return true;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper() || _.Name.ToUpper() == info.ColumnName.ToUpper())
                .Where(_ => _.GetType().GetProperty(propertyVerification).GetValue(_, null).ToString().Equals(conditionProperty))
                .IsAny();


        }
    }
}
