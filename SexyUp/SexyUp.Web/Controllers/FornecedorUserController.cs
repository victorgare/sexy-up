using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Enum;
using SexyUp.Infrastructure.Context;
using SexyUp.Utils.Utils;
using SexyUp.Web.Controllers.Common;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.FornecedorUser;

namespace SexyUp.Web.Controllers
{
    [Authorize(Roles = nameof(Roles.Administrador)), ValidateAntiForgeryToken]
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

        [HttpPost]
        public ActionResult Register(FornecedorUserViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.Email,
                    CpfCnpj = Mask.RemoveMask(viewModel.Cnpj),
                    PhoneNumber = viewModel.PhoneNumber,
                    PhantasyName = viewModel.PhantasyName,
                    Site = viewModel.Site
                };


                var result = UserManager.Create(user, "Fornecedor@1234");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, nameof(Roles.Fornecedor));

                    FlashMessage.Success("Cadastrado com sucesso");
                    return RedirectToAction(nameof(Index), "FornecedorUser");
                }

                FlashMessage.Error(result.Errors.FirstOrDefault());

            }

            if (viewModel.Errors.Any())
            {
                FlashMessage.Error(viewModel.Errors.Any()
                    ? viewModel.Errors.FirstOrDefault()
                    : "O formulário não está valido");
            }

            return View(nameof(Register), viewModel);
        }
        #endregion

        #region Edit

        public ActionResult Edit(string id)
        {
            var user = UserManager.FindById(id);

            var viewModel = new FornecedorUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Cnpj = user.CpfCnpj,
                PhoneNumber = user.PhoneNumber,
                PhantasyName = user.PhantasyName,
                Site = user.Site
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(FornecedorUserViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel.IsValid)
            {
                var user = UserManager.FindById(viewModel.Id);

                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.Email = viewModel.Email;
                user.UserName = viewModel.Email;
                user.CpfCnpj = viewModel.Cnpj;
                user.PhoneNumber = viewModel.PhoneNumber;
                user.PhantasyName = viewModel.PhantasyName;
                user.Site = viewModel.Site;

                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    FlashMessage.Success("Salvo com sucesso");
                    return RedirectToAction(nameof(Index), "FornecedorUser");
                }

                FlashMessage.Error(result.Errors.FirstOrDefault());
            }

            if (viewModel.Errors.Any())
            {
                FlashMessage.Error(viewModel.Errors.Any()
                    ? viewModel.Errors.FirstOrDefault()
                    : "O formulário não está valido");
            }

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