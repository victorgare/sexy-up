using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Controllers.Common;
using SexyUp.Web.ViewModels.Cart;
using SexyUp.Web.ViewModels.Order;

namespace SexyUp.Web.Controllers
{
    public class OrderController : BaseAccountController
    {

        private readonly ITransactionService _transactionService;

        public OrderController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public ActionResult Cart(List<CartViewModel> cartItens)
        {
            return View(cartItens);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            // se a request não view do carrinho ou da pagina de Login
            // direciona pra pagina inicial
            var urlReferrer = Request.UrlReferrer?.AbsolutePath;
            if (urlReferrer != Url.Action(nameof(Cart), "Order")
                && urlReferrer != Url.Action("Login", "Account")
                && urlReferrer != Url.Action(nameof(PaymentMethod), "Order"))
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            user.PasswordHash = string.Empty;

            var viewModel = new CheckoutViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(viewModel);
        }

        [Authorize, HttpPost]
        public ActionResult Checkout(CheckoutViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            user.ZipCode = viewModel.ZipCode;
            user.Street = $"{viewModel.Street}, {viewModel.Number}";

            UserManager.Update(user);

            return RedirectToAction(nameof(PaymentMethod), "Order");
        }

        [Authorize]
        public ActionResult PaymentMethod()
        {
            var urlReferrer = Request.UrlReferrer?.AbsolutePath;
            if (urlReferrer != Url.Action(nameof(Checkout), "Order")
                && urlReferrer != Url.Action(nameof(OrderReview), "Order"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize, HttpPost]
        public ActionResult OrderReview(List<CartViewModel> cartItens)
        {
            var urlReferrer = Request.UrlReferrer?.AbsolutePath;
            if (urlReferrer != Url.Action(nameof(PaymentMethod), "Order"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(cartItens);
        }


        [Authorize, HttpPost]
        public ActionResult PlaceOrder(List<CartViewModel> cartItens)
        {

            var urlReferrer = Request.UrlReferrer?.AbsolutePath;
            if (urlReferrer != Url.Action(nameof(OrderReview), "Order"))
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            // monta a transacao
            var transaction = new Transaction
            {
                DeliveryAddress = user.Street,
                IdUser = userId,
                TotalPrice = cartItens.Sum(c => c.ProductPrice * c.Quantity) + 20
            };

            // monta a lista de itens da compra
            var transactionItens = new List<TransactionItens>();
            cartItens.ForEach(c => transactionItens.Add(new TransactionItens
            {
                Quantity = c.Quantity,
                IdProduct = c.ProductId
            }));

            // adiciona a ordem
            _transactionService.PlaceOrder(transaction, transactionItens);

            return RedirectToAction("Index", "Dashboard");
        }


        public ActionResult OrderDetails(string orderId)
        {
            var userId = User.Identity.GetUserId();
            var order = _transactionService.GetTransactionByTransactionIdAndUserId(orderId, userId);

            return View(order);
        }
    }
}