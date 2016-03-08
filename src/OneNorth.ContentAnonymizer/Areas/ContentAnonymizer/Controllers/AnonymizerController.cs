using System.Web.Mvc;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Controllers
{
    [AuthorizeAnonymizer]
    public class AnonymizerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/ContentAnonymizer/Views/Anonymizer/Index.cshtml");
        }
    }
}