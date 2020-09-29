using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen.Structural
{
    public static class HelperSysObjectUtil
    {
        public static string CamelCaseTransform(string value)
        {
            value = string.Concat(value
                           .Split('_')
                           .Where(_ => !string.IsNullOrEmpty(_))
                           .Select(y => y.Substring(0, 1).ToUpper() + y.Substring(1) + "_"));

            return value;
        }
        public static string ClassNameLowerAndSeparator(string className)
        {
            return className.ToLower();
        }
        public static string MakeKeyNames(TableInfo tableInfo, EOperation operation)
        {
            var keys = string.Empty;
            if (tableInfo.Keys != null)
            {
                if (tableInfo.Keys.IsAny())
                {
                    foreach (var item in tableInfo.Keys)
                        keys += string.Format("model.{0},", item);

                    keys = keys.Substring(0, keys.Length - 1);
                }
            }
            return keys;
        }
        public static string ParametersKeyNames(TableInfo tableInfo, bool camelCasing, EOperation operation = EOperation.Undefined, string variable = "item", Func<string,string> camelCaseTransform = null)
        {
            var parametersInit = "{";
            var parametersContent = "";
            var parametersEnd = " }";

            if (tableInfo.Keys.IsAny())
            {
                foreach (var item in tableInfo.Keys)
                {
                    var key = camelCasing ? camelCaseTransform != null ? camelCaseTransform(item) : CamelCaseTransform(item) : item;
                    var keyFilterName = tableInfo.Keys.Count() == 1 ? "id" : key;
                    parametersContent += string.Format(" {0}: {1}.{2},", keyFilterName, variable, key);
                }

                parametersContent = parametersContent.Substring(0, parametersContent.Length - 1);
            }
            var parameters = parametersInit + parametersContent + parametersEnd;
            return parameters;
        }
        public static string ExpressionKeyNames(TableInfo tableInfo, bool camelCasing, EOperation operation = EOperation.Undefined)
        {
            return "define yourself";
        }
        public static string MakeKeysFromGet(TableInfo tableInfo, string classFilter = "model")
        {
            var keys = string.Empty;
            if (tableInfo.Keys.IsAny())
            {
                foreach (var item in tableInfo.Keys)
                    keys += string.Format(".Where(_=>_.{0} == {1}.{2})", item, classFilter, item);
            }
            return keys;
        }
        public static string OrderyByKeys(TableInfo tableInfo, string classFilter = "model")
        {
            var keys = string.Empty;
            if (tableInfo.Keys.IsAny())
            {
                var index = 0;
                foreach (var item in tableInfo.Keys)
                {
                    var orderClause = "OrderBy";
                    if (index > 0)
                        orderClause = "ThenBy";

                    keys += string.Format(".{0}(_ => _.{1})", orderClause, item);
                    index++;
                }
            }
            return keys;
        }
        public static string ToolName(TableInfo tableInfo)
        {
            return !string.IsNullOrEmpty(tableInfo.ToolsName) ? string.Format("base.toolName = {0};", tableInfo.ToolsName) : string.Format("base.toolName = {0};", "string.Empty");
        }
        public static string MakeReletedNamespace(TableInfo tableInfo, Context configContext, string classBuilder)
        {
            var namespaceReletedApp = string.Empty;
            var namespaceReletedAppTest = string.Empty;
            var reletedClass = tableInfo.ReletedClass;

            if (reletedClass != null)
            {
                foreach (var item in reletedClass.Where(_ => _.NavigationType == NavigationType.Instance))
                {

                    if (item.NamespaceApp != configContext.Namespace)
                        namespaceReletedApp = !string.IsNullOrEmpty(item.NamespaceApp) ? string.Format("using {0}.Application;", item.NamespaceApp) : string.Empty;

                    if (item.NamespaceApp != configContext.Namespace)
                        namespaceReletedAppTest = !string.IsNullOrEmpty(item.NamespaceApp) ? string.Format("using {0}.Application.Test;", item.NamespaceApp) : string.Empty;

                }
            }

            classBuilder = classBuilder.Replace("<#namespaceReleted#>", namespaceReletedApp);
            classBuilder = classBuilder.Replace("<#namespaceReletedTest#>", namespaceReletedAppTest);

            return classBuilder;
        }

    }
}
