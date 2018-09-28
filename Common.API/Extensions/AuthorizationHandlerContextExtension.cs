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
    public static class AuthorizationHandlerContextExtension
    {


        public static Boolean VerifyClaimsCanRead(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanRead(DefineControllerName(source));

            return false;
        }
       

        public static Boolean VerifyClaimsCanWrite(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanWrite(DefineControllerName(source));

            return false;
        }

        public static Boolean VerifyClaimsCanDelete(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanDelete(DefineControllerName(source));

            return false;
        }

        private static string DefineControllerName(AuthorizationHandlerContext source)
        {
            return ((ControllerActionDescriptor)((ActionContext)source.Resource).ActionDescriptor).ControllerName;
        }


    }
}
