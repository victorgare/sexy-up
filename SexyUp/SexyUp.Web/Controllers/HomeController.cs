using System.Web.Mvc;
using SexyUp.Web.ViewModels.Home;

namespace SexyUp.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(HomeViewModel homeViewModel)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}