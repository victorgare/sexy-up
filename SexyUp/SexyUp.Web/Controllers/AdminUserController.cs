using System;
using System.Linq;
using SexyUp.Web.Controllers.Common;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.Infrastructure.Context;
using SexyUp.Web.Libraries.FlashMessage;

namespace SexyUp.Web.Controllers
{
    [Authorize]
    public class AdminUserController : BaseAccountController
    {

        public ActionResult Index()
        {
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDatabaseContext())))
            {
                var role = roleManager.FindByName("Administrador").Users.FirstOrDefault();

                var users = UserManager.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();

                return View(users);

            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var user = UserManager.FindById(id);
            return View();
        }

        public ActionResult Lock(string id)
        {

            var user = UserManager.FindById(id);
            user.LockoutEndDateUtc = DateTime.MaxValue;

            var result = UserManager.Update(user);

            if (result.Succeeded)
            {
                FlashMessage.Success("Bloqueado com sucesso");
            }
            else
            {
                FlashMessage.Error(result.Errors.FirstOrDefault());
            }

            return RedirectToAction(nameof(Index), "AdminUser");
        }

        public ActionResult Unlock(string id)
        {
            var user = UserManager.FindById(id);
            user.LockoutEndDateUtc = DateTime.MaxValue;

            var result = UserManager.Update(user);

            if (result.Succeeded)
            {
                FlashMessage.Success("Desbloqueado com sucesso");
            }
            else
            {
                FlashMessage.Error(result.Errors.FirstOrDefault());
            }

            return RedirectToAction(nameof(Index), "AdminUser");
        }

    }
}