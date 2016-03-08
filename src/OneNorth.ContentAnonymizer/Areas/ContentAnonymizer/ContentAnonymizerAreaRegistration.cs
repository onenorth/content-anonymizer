using System.Web.Http;
using System.Web.Mvc;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer
{
    public class ContentAnonymizerAreaRegistration : AreaRegistration
    {
        private const string ContentAnonymizerAreaName = "ContentAnonymizer";

        public override string AreaName
        {
            get { return ContentAnonymizerAreaName; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapRoute(
                "contentanonymizer_api",
                "sitecore/admin/contentanonymizer/api/{action}/{id}",
                new { area = ContentAnonymizerAreaName, controller = "AnonymizerApi", action = "Index", id = RouteParameter.Optional });

            context.Routes.MapRoute(
                "contentanonymizer",
                "sitecore/admin/contentanonymizer/{action}/{id}",
                new { area = ContentAnonymizerAreaName, controller = "Anonymizer", action = "Index", id = RouteParameter.Optional });

            
        }
    }
}