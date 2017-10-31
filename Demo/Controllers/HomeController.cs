using System.Web.Mvc;

namespace Demo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "UserTexts");
        }
    }
}