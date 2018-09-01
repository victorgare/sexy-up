using System;
using System.Linq;
using SexyUp.Web.Controllers.Common;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.ApplicationCore.Entities;
using SexyUp.Infrastructure.Context;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.AdminUser;

namespace SexyUp.Web.Controllers
{
    //[Authorize]
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

        #region Register

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AdminUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                FlashMessage.Error("Preencha todos os campos");
                return View(nameof(Register), viewModel);
            }

            var user = new ApplicationUser
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                UserName = viewModel.Email
            };

            var result = UserManager.Create(user, "Admin@1234");
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "Administrador");

                FlashMessage.Success("Criado com sucesso");

                return RedirectToAction("Index", "AdminUser");
            }

            FlashMessage.Error(result.Errors.FirstOrDefault());
            return View("Register", viewModel);
        }

        #endregion

        #region Edit

        public ActionResult Edit(string id)
        {
            var user = UserManager.FindById(id);

            var viewModel = new AdminUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AdminUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                FlashMessage.Error("Preencha todos os campos");
                return View(nameof(Edit), viewModel);
            }

            var user = UserManager.FindById(viewModel.Id);

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.UserName = viewModel.Email;

            var result = UserManager.Update(user);

            if (result.Succeeded)
            {
                FlashMessage.Success("Alterado com sucesso");
                return RedirectToAction(nameof(Index), "AdminUser");
            }

            FlashMessage.Error(result.Errors.FirstOrDefault());
            return View(nameof(Edit), viewModel);
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