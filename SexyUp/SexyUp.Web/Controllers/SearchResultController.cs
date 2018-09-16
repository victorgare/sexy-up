using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Helpers;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.SearchResult;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace SexyUp.Web.Controllers
{
    public class SearchResultController : Controller
    {
        private readonly IProductService _productService;

        public SearchResultController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult SearchResult()
        {
            return View();
        }

        public ActionResult Search(SearchResultViewModel searchResultViewModel)
        {
            var searchTerm = searchResultViewModel.SearchTerm;
            var lastSearchTerm = searchResultViewModel.LastSearchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(SearchResult), "SearchResult");
            }

            var page = searchResultViewModel.Page;

            // se o termo de pesquisa atual for diferente do anterior
            // retorna a paginação para a primeira pagina
            if (searchTerm != lastSearchTerm)
            {
                page = 0;
            }

            var pageIndex = (page == 0 ? 1 : page) - 1;

            var categories = searchResultViewModel.CategoriesSelected;
            var minValue = searchResultViewModel.MinPriceSelected;
            var maxValue = searchResultViewModel.MaxPriceSelected;

            var itens = _productService.SearchTerm(searchTerm, categories, minValue, maxValue, pageIndex, 12, out var totalItens);

            if (!itens.Any() && searchTerm == lastSearchTerm)
            {
                FlashMessage.Warning("Nenhum produto encontrado");

                var result = new StaticPagedList<Product>(itens, pageIndex + 1, 12, totalItens);
                searchResultViewModel.SearchResult = result;

                FillFilterOptions(ref searchResultViewModel);
            }
            else if (!itens.Any())
            {
                FlashMessage.Warning("Nenhum produto encontrado");
            }
            else
            {
                var result = new StaticPagedList<Product>(itens, pageIndex + 1, 12, totalItens);
                searchResultViewModel.SearchResult = result;

                FillFilterOptions(ref searchResultViewModel);
            }

            searchResultViewModel.LastSearchTerm = searchTerm;
            return View(nameof(SearchResult), searchResultViewModel);
        }

        private void FillFilterOptions(ref SearchResultViewModel searchResultViewModel)
        {
            const string categoriesKey = "CategoriesCookies";
            const string pricesKey = "PricesCookies";

            var searchTerm = searchResultViewModel.SearchTerm;
            var lastSearchTerm = searchResultViewModel.LastSearchTerm;

            List<Category> categories;
            List<decimal> prices;

            if (searchTerm != lastSearchTerm)
            {
                categories = _productService.GetAllCategoriesBySeachTerm(searchResultViewModel.SearchTerm);
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
                    categories = _productService.GetAllCategoriesBySeachTerm(searchResultViewModel.SearchTerm);
                }

                if (prices == null)
                {
                    prices = _productService.GetAllPricesBySearchTerm(searchTerm);
                }
            }

            searchResultViewModel.MinPrice = prices[0];
            searchResultViewModel.MaxPrice = prices[1];
            searchResultViewModel.CategoriesMultiSelect = new MultiSelectList(categories, "Id", "Name");
        }
    }
}