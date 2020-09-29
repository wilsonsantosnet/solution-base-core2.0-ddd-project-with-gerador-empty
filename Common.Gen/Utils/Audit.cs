using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class Audit
    {
        private static string[] auditFieldsDefault = new string[] { "UserCreateId", "UserCreateDate", "UserAlterId", "UserAlterDate" };
        private static Dictionary<string, string> Mapper { get; set; }
        private static string[] auditFields = auditFieldsDefault;

        public static void SetAuditFields(params string[] fields)
        {
            auditFields = fields;
            CreateMapper();
        }

        public static string[] GetAuditFields()
        {
            return auditFields;
        }

        public static string DefinePropertyDefault(string PropertyName)
        {
            if (GetAuditFields().Where(_ => _ == PropertyName).IsAny())
                return Mapper[PropertyName];

            return PropertyName;
        }

        public static string MakeAuditRow(TableInfo tableInfo, bool generateAudit, Info item, string textTemplateAudit, DefineTemplateFolder defineTemplateFolder)
        {
            if (generateAudit)
                return MakeAuditFields(tableInfo, item, textTemplateAudit, defineTemplateFolder);

            return string.Empty;
        }

        public static bool ExistsAuditFieldsDefault(IEnumerable<Info> infos)
        {
            return ExistsFields(infos, auditFieldsDefault);
        }

        public static bool ExistsAuditFields(IEnumerable<Info> infos)
        {
            return ExistsFields(infos, auditFields);
        }

        public static bool IsAuditField(string field)
        {
            return auditFieldsDefault.Contains(field);
        }

        public static bool IsNotAuditField(string propertyName)
        {
            return !IsAuditField(propertyName);
        }

        public static string MakeAuditFields(TableInfo tableInfo, Info item, string textTemplateAudit, DefineTemplateFolder defineTemplateFolder)
        {
            var pathTemplateAudit = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsAudit(tableInfo));

            if (string.IsNullOrEmpty(textTemplateAudit))
                textTemplateAudit = Read.AllText(tableInfo, pathTemplateAudit, defineTemplateFolder);

            textTemplateAudit = textTemplateAudit.Replace("<#className#>", tableInfo.ClassName);

            if (item.PropertyName == "UserCreateId" || item.PropertyName == "UserAlterId")
            {
                var cast = String.Format("({0})userId", TypeConvertCSharp.Convert(item.TypeOriginal, item.IsNullable));

                if (item.PropertyName == "UserCreateId")
                    textTemplateAudit = textTemplateAudit.Replace("<#propertCastInsert#>", cast);

                if (item.PropertyName == "UserAlterId")
                    textTemplateAudit = textTemplateAudit.Replace("<#propertCastUpdate#>", cast);

            }
            return textTemplateAudit;
        }

        private static bool ExistsFields(IEnumerable<Info> infos, params string[] fields)
        {
            var existsFields = false;

            foreach (var item in fields)
            {
                existsFields = infos
                    .Where(_ => _.PropertyName.IsNotNull())
                    .Where(_ => _.PropertyName.Equals(item))
                    .Any();

                if (!existsFields)
                    return false;

                //var info = infos.Where(_ => _.PropertyName == item).SingleOrDefault();
                //if (info.IsNotNull())
                //{
                //    if (auditFields[0] == info.PropertyName && info.isNullable == 1) return false;
                //    if (auditFields[1] == info.PropertyName && info.isNullable == 1) return false;
                //    if (auditFields[2] == info.PropertyName && info.isNullable == 0) return false;
                //    if (auditFields[3] == info.PropertyName && info.isNullable == 0) return false;
                //}
            }

            return true;
        }

        private static void CreateMapper()
        {
            Mapper = new Dictionary<string, string>
            {
                { auditFields[0], auditFieldsDefault[0] },
                { auditFields[1], auditFieldsDefault[1] },
                { auditFields[2], auditFieldsDefault[2] },
                { auditFields[3], auditFieldsDefault[3] }
            };
        }


    }
}
