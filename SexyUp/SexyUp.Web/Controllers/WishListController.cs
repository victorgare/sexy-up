using System;
using SexyUp.ApplicationCore.Interfaces.Service;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SexyUp.Web.Libraries.FlashMessage;

namespace SexyUp.Web.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var list = _wishListService.GetUsersWishList(userId);
            return View(list);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                _wishListService.Delete(id);

                FlashMessage.Success("Excluido com sucesso");
            }
            catch (Exception e)
            {
                FlashMessage.Error(e.Message);
            }
            return RedirectToAction(nameof(Index), "WishList");
        }
    }
}