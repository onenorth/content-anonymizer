using System.Web.Http;
using System.Web.Mvc;

namespace OneNorth.DataAnonymizer.Areas.DataAnonymizer
{
    public class DataAnonymizerAreaRegistration : AreaRegistration
    {
        private const string DataAnonymizerAreaName = "DataAnonymizer";

        public override string AreaName
        {
            get { return DataAnonymizerAreaName; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapRoute(
                "dataanonymizer_api",
                "sitecore/admin/dataanonymizer/api/{action}/{id}",
                new { area = DataAnonymizerAreaName, controller = "DataAnonymizerApi", action = "Index", id = RouteParameter.Optional });

            context.Routes.MapRoute(
                "dataanonymizer",
                "sitecore/admin/dataanonymizer/{action}/{id}",
                new { area = DataAnonymizerAreaName, controller = "DataAnonymizer", action = "Index", id = RouteParameter.Optional });

            
        }
    }
}