using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoysCoffe.Custom_Attribute
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Kiểm tra nếu session "Role" tồn tại và là "Admin"
            var role = httpContext.Session["Role"];
            return role != null && role.ToString() == "Admin";
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Chuyển hướng đến trang Unauthorized nếu không đủ quyền
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Secure" },
                    { "action", "Unauthorized" }
                });
        }
    }
}