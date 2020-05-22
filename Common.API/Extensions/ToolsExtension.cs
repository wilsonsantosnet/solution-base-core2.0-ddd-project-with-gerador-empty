using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.API.Extensions
{
    public static class ToolsExtension
    {

        public static Boolean VerifyClaimsCanReadOne(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanReadOne).IsAny();

            return false;
        }

        public static Boolean VerifyClaimsCanReadDataItem(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanReadDataItem).IsAny();

            return false;
        }

        public static Boolean VerifyClaimsCanReadCustom(this IEnumerable<Tool> tools, string controllerName)
        {
            var canCustomRead = false;
            var canRead = false;

            if (tools.IsAny())
                canRead = Verify(controllerName, tools).Where(_ => _.CanReadAll).IsAny();

            if (tools.IsAny())
                canCustomRead = Verify(controllerName, tools).Where(_ => _.CanReadCustom).IsAny();

            return (canRead || canCustomRead) ? true : false;
        }

        public static Boolean VerifyClaimsCanReadAll(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanReadAll).IsAny();

            return false;
        }

        public static Boolean VerifyClaimsCanEdit(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanEdit).IsAny();

            return false;
        }

        public static Boolean VerifyClaimsCanSave(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanSave).IsAny();

            return false;
        }

        public static Boolean VerifyClaimsCanWrite(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanWrite).IsAny();

            return false;
        }

        public static Boolean VerifyClaimsCanDelete(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanDelete).IsAny();

            return false;
        }


        private static IEnumerable<Tool> Verify(string controllerName, IEnumerable<Tool> tools)
        {
            return tools.Where(_ => _.Key.ToLower() == controllerName.Replace("More", "").ToLower());
        }

    }
}
