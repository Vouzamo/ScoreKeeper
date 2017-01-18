using Microsoft.AspNetCore.Mvc;

namespace Vouzamo.ScoreKeeper.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "League");
        }
    }
}
