using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Controllers.Common;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.Cart;
using SexyUp.Web.ViewModels.Order;

namespace SexyUp.Web.Controllers
{
    public class OrderController : BaseAccountController
    {

        private readonly ITransactionService _transactionService;
        private readonly ICouponService _couponService;
        private readonly IProductService _productService;

        public OrderController(ITransactionService transactionService, ICouponService couponService, IProductService productService)
        {
            _transactionService = transactionService;
            _couponService = couponService;
            _productService = productService;
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
        public ActionResult PlaceOrder(List<CartViewModel> cartItens, string couponName)
        {

            var urlReferrer = Request.UrlReferrer?.AbsolutePath;
            if (urlReferrer != Url.Action(nameof(OrderReview), "Order"))
            {
                return RedirectToAction("Index", "Home");
            }

            var coupon = _couponService.FindByName(couponName);

            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);


            var totalPrice = 0m;

            foreach (var cartItem in cartItens)
            {
                var product = _productService.GetById(cartItem.ProductId);
                if (coupon != null && coupon.Valid)
                {
                    totalPrice += (100 - coupon.DiscountPercentage) / 100 * product.Price * cartItem.Quantity;
                }
                else
                {
                    totalPrice += product.Price * cartItem.Quantity;
                }
            }

            // adiciona o frete
            totalPrice += 20;

            // monta a transacao
            var transaction = new Transaction
            {
                DeliveryAddress = user.Street,
                IdUser = userId,
                TotalPrice = totalPrice,
                CouponId = coupon?.Id
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

        [Authorize, HttpPost]
        public JsonResult ValidateCoupon(string couponName)
        {
            var coupon = _couponService.FindByName(couponName);

            if (coupon != null && coupon.Valid)
            {
                return Json(new
                {
                    Valid = true,
                    Message = $"Adicionado {coupon.DiscountPercentage}% de desconto",
                    DisctountPercentage = coupon.DiscountPercentage
                });
            }

            return Json(new
            {
                Valid = false,
                Message = "Coupon inválido",
                DisctountPercentage = 0.0
            });
        }
    }
}