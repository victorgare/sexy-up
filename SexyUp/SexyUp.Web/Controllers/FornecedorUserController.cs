using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.ApplicationCore.Enum;
using SexyUp.Infrastructure.Context;
using SexyUp.Web.Controllers.Common;
using SexyUp.Web.Libraries.FlashMessage;

namespace SexyUp.Web.Controllers
{
    [Authorize(Roles = nameof(Roles.Administrador))]
    public class FornecedorUserController : BaseAccountController
    {
        public ActionResult Index()
        {
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDatabaseContext())))
            {
                var role = roleManager.FindByName(nameof(Roles.Fornecedor));

                var users = UserManager.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.Id)).ToList();

                return View(users);

            }
        }

        #region Register


        public ActionResult Register()
        {
            return View();
        }
        #endregion

        #region Edit

        public ActionResult Edit(string id)
        {
            return View();
        }

        #endregion
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

            return RedirectToAction(nameof(Index), "FornecedorUser");
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

            return RedirectToAction(nameof(Index), "FornecedorUser");
        }

    }
}