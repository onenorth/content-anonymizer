using System;
using System.Web;
using System.Web.Mvc;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAnonymizerAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.RawUrl;
            var url = string.Format("/sitecore/admin/login.aspx?returnUrl={0}", HttpUtility.UrlEncode(returnUrl));
            filterContext.Result = new RedirectResult(url);
        }
    }
}