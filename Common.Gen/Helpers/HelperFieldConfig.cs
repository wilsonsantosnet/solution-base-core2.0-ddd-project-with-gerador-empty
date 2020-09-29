using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public class HelperFieldConfig
    {

        public static bool IsPassword(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Password)
                .IsAny();
        }

        public static bool IsPasswordConfirmation(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.PasswordConfirmation)
                .IsAny();
        }

        
        public static bool FieldInBlackList(TableInfo tableInfo, Info info, string propertyName, EOperation blackList)
        {
            return HelperRestrictions.RunRestrictions(FieldConfigShow.ShowAll, tableInfo, info, propertyName, blackList);
        }
        
        
        public static bool FieldInAllowList(TableInfo tableInfo, Info info, string propertyName, EOperation AllowList)
        {
            return HelperRestrictions.RunRestrictions(FieldConfigShow.HideAll, tableInfo, info, propertyName, AllowList);
        }


        public static bool IgnoreBigLength(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.IgnoreBigLength == true)
                .IsAny();
        }

        public static bool IsEmail(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Email)
                .IsAny();
        }

        public static bool IsAttr(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Attributes.IsAny())
                .IsAny();
        }

        public static string GetAttr(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return string.Empty;

            var attr = tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper()).SelectMany(_ => _.Attributes);

            return string.Join(" ", attr);
        }

        public static bool IsAttrSection(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.AttributesSection.IsAny())
                .IsAny();
        }

        public static bool IsAttrFilter(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.AttributesFilters.IsAny())
                .IsAny();
        }

        public static string GetAttrFilter(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return string.Empty;

            var attr = tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper()).SelectMany(_ => _.AttributesFilters);

            return string.Join(" ", attr);
        }

        public static string GetAttrSection(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return string.Empty;

            var attr = tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper()).SelectMany(_ => _.AttributesSection);

            return string.Join(" ", attr);
        }

        public static string GetHtmlAfterSection(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return string.Empty;

            var html = tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper()).Select(_ => _.InsertHtmlAfterSection);

            return string.Join(" ", html);
        }

        public static HtmlCtrl FieldHtml(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return null;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.HTML.IsNotNull())
                .Select(_ => _.HTML).SingleOrDefault();
        }

        public static int GetColSizeField(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return 0;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.ColSize.IsSent())
                .Select(_ => _.ColSize)
                .DefaultIfEmpty()
                .SingleOrDefault();
        }

        public static bool IsRadio(TableInfo tableInfo, string propertyName)
        {
            
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Radio)
                .IsAny();
        }

        public static FieldConfig GetField(TableInfo tableInfo, string propertyName)
        {

            if (tableInfo.FieldsConfig.IsNotAny())
                return null;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .SingleOrDefault();
        }



        public static bool IsUpload(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Upload)
                .IsAny();
        }

        public static bool IsSelectSearch(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.SelectSearch)
                .IsAny();
        }

        public static bool IsMultiSelectFilter(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.MultiSelectFilter)
                .IsAny();
        }

        public static bool IsTextEditor(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.TextEditor)
                .IsAny();
        }

        public static bool IsTextTag(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Tags)
                .IsAny();
        }

        public static bool IsTextStyle(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.TextStyle)
                .IsAny();
        }

        public static Dictionary<string, string> FieldDataItem(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return new Dictionary<string, string>();

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.DataItem.IsAny())
                .Select(_ => _.DataItem).SingleOrDefault();
        }
    }
}
