using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Core.Security
{
   public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect("/Login");
            }

            else if (context.HttpContext.User.FindFirst("RoleId") == null || context.HttpContext.User.FindFirst("RoleId").Value != "Admin")
            {
                context.HttpContext.Response.Redirect("/Login");
            }

        }
    }
}
