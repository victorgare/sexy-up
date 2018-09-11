using System.Web.Mvc;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.ViewModels.Home;

namespace SexyUp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(HomeViewModel homeViewModel)
        {
            var itens = _productService.SearchTerm(homeViewModel.SearchTerm);

            homeViewModel.SearchResult = itens;
            return View(nameof(Index), homeViewModel);
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