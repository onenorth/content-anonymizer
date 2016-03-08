using System;
using System.Web.Mvc;

namespace OneNorth.DataAnonymizer.Areas.DataAnonymizer.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeDataAnonymizerApiAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpStatusCodeResult(403);
        }
    }
}