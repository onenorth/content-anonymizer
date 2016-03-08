using System.Web.Mvc;
using OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models;

namespace OneNorth.DataAnonymizer.Areas.DataAnonymizer.Controllers
{
    [AuthorizeDataAnonymizer]
    public class DataAnonymizerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new DataAnonymizerIndexViewModel();

            return View("~/Areas/DataAnonymizer/Views/DataAnonymizer/Index.cshtml", model);
        }
    }
}