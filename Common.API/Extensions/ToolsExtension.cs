using Common.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Common.API.Extensions
{
    public static class ToolsExtension
    {


        public static Boolean VerifyClaimsCanRead(this IEnumerable<Tool> tools, string controllerName)
        {
            if (tools.IsAny())
                return Verify(controllerName, tools).Where(_ => _.CanRead).IsAny();

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
