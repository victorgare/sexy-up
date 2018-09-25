using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Helpers;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.Home;
using SexyUp.Web.ViewModels.SexyService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace SexyUp.Web.Controllers
{
    public class SexyServiceController : Controller
    {
        private readonly ISexyServiceService _sexyServiceService;

        public SexyServiceController(ISexyServiceService sexyServiceService)
        {
            _sexyServiceService = sexyServiceService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SexyServiceViewModel sexyServiceViewModel)
        {
            var searchTerm = sexyServiceViewModel.SearchTerm;
            var lastSearchTerm = sexyServiceViewModel.LastSearchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(Index), "SexyService");
            }

            var page = sexyServiceViewModel.Page;

            // se o termo de pesquisa atual for diferente do anterior
            // retorna a paginação para a primeira pagina
            if (searchTerm != lastSearchTerm)
            {
                page = 0;
            }

            var pageIndex = (page == 0 ? 1 : page) - 1;

            var itens = _sexyServiceService.SearchTerm(searchTerm, pageIndex, 12, out var totalItens);

            if (!itens.Any() && searchTerm == lastSearchTerm)
            {
                FlashMessage.Warning("Nenhum serviço encontrado");

                var result = new StaticPagedList<SexyService>(itens, pageIndex + 1, 12, totalItens);
                sexyServiceViewModel.SearchResult = result;

                //FillFilterOptions(ref sexyServiceViewModel);
            }
            else if (!itens.Any())
            {
                FlashMessage.Warning("Nenhum serviço encontrado");
            }
            else
            {
                var result = new StaticPagedList<SexyService>(itens, pageIndex + 1, 12, totalItens);
                sexyServiceViewModel.SearchResult = result;

                //FillFilterOptions(ref sexyServiceViewModel);
            }

            sexyServiceViewModel.LastSearchTerm = searchTerm;
            return View(nameof(Index), sexyServiceViewModel);
        }

        public ActionResult SexyServiceDetail(string id)
        {
            var sexyService = _sexyServiceService.GetById(id);
            return View(sexyService);
        }
    }
}