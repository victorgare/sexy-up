using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Helpers;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.Home;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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

            var categories = homeViewModel.CategoriesSelected;
            var minValue = homeViewModel.MinPriceSelected;
            var maxValue = homeViewModel.MaxPriceSelected;

            var itens = _productService.SearchTerm(searchTerm, categories, minValue, maxValue, pageIndex, 12, out var totalItens);

            if (!itens.Any() && searchTerm == lastSearchTerm)
            {
                FlashMessage.Warning("Nenhum produto encontrado");

                var result = new StaticPagedList<Product>(itens, pageIndex + 1, 12, totalItens);
                homeViewModel.SearchResult = result;

                FillFilterOptions(ref homeViewModel);
            }
            else if (!itens.Any())
            {
                FlashMessage.Warning("Nenhum produto encontrado");
            }
            else
            {
                var result = new StaticPagedList<Product>(itens, pageIndex + 1, 12, totalItens);
                homeViewModel.SearchResult = result;

                FillFilterOptions(ref homeViewModel);
            }

            homeViewModel.LastSearchTerm = searchTerm;
            return View(nameof(Index), homeViewModel);
        }

        private void FillFilterOptions(ref HomeViewModel homeViewModel)
        {
            const string categoriesKey = "CategoriesCookies";
            const string pricesKey = "PricesCookies";

            var searchTerm = homeViewModel.SearchTerm;
            var lastSearchTerm = homeViewModel.LastSearchTerm;

            List<Category> categories;
            List<decimal> prices;

            if (searchTerm != lastSearchTerm)
            {
                categories = _productService.GetAllCategoriesBySeachTerm(homeViewModel.SearchTerm);
                prices = _productService.GetAllPricesBySearchTerm(searchTerm);

                SessionMenager.SetObject(categories, categoriesKey);
                SessionMenager.SetObject(prices, pricesKey);

            }
            else
            {
                categories = SessionMenager.GetObjectsList<Category>(categoriesKey);
                prices = SessionMenager.GetObjectsList<decimal>(pricesKey);

                if (categories == null)
                {
                    categories = _productService.GetAllCategoriesBySeachTerm(homeViewModel.SearchTerm);
                }

                if (prices == null)
                {
                    prices = _productService.GetAllPricesBySearchTerm(searchTerm);
                }
            }

            homeViewModel.MinPrice = prices[0];
            homeViewModel.MaxPrice = prices[1];
            homeViewModel.CategoriesMultiSelect = new MultiSelectList(categories, "Id", "Name");
        }

        public ActionResult ProductDetail(string id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }
    }
}