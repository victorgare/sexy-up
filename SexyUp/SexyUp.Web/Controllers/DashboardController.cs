using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.ViewModels.Dashboard;

namespace SexyUp.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITransactionService _transactionService;

        public DashboardController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel();

            // se a requisição veio do metodo place order
            // quer dizer que um novo pedido foi inserido, entao devemos limpar os itens do carrinho
            // faremos isso setando NewOrder = true no viewModel
            var urlReferrer = Request.UrlReferrer?.AbsolutePath;
            if (urlReferrer == Url.Action("OrderReview", "Order"))
            {
                viewModel.NewOrderPlaced = true;
            }

            var userId = User.Identity.GetUserId();
            viewModel.Transactions = _transactionService.GetUserTransactions(userId);

            return View(viewModel);
        }
    }
}