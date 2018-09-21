using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SexyUp.Web.Controllers.Common;
using SexyUp.Web.ViewModels.Cart;
using SexyUp.Web.ViewModels.Order;

namespace SexyUp.Web.Controllers
{
    public class OrderController : BaseAccountController
    {
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
            return View();
        }
    }
}