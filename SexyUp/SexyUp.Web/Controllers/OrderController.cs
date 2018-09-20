using System.Collections.Generic;
using System.Web.Mvc;
using SexyUp.Web.ViewModels.Cart;

namespace SexyUp.Web.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public ActionResult Cart(List<CartViewModel> cartItens)
        {
            return View(cartItens);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            if (Request.UrlReferrer?.AbsolutePath == Url.Action(nameof(Cart), "Order"))
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}