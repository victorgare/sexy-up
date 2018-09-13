using System.Linq;
using System.Web.Mvc;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.Home;
using X.PagedList;

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
            var searchTerm = homeViewModel.SearchTerm;
            var lastSearchTerm = homeViewModel.LastSearchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            var page = homeViewModel.Page;

            // se o termo de pesquisa atual for diferente do anterior
            // retorna a paginação para a primeira pagina
            if (searchTerm != lastSearchTerm)
            {
                page = 0;
            }

            var pageIndex = (page == 0 ? 1 : page) - 1;

            var itens = _productService.SearchTerm(homeViewModel.SearchTerm, pageIndex, 12, out var totalItens);

            if (!itens.Any())
            {
                FlashMessage.Warning("Nenhum produto encontrado");
            }
            else
            {
                var result = new StaticPagedList<Product>(itens, pageIndex + 1, 12, totalItens);
                homeViewModel.SearchResult = result;
            }

            homeViewModel.LastSearchTerm = searchTerm;
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