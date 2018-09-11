using System;
using System.Collections.Generic;
using SexyUp.ApplicationCore.Constants;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.Product;

namespace SexyUp.Web.Controllers
{
    [Authorize(Roles = Roles.AdministradorAndFornecedor)]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            List<Product> lista;

            if (User.IsInRole(Roles.Administrador))
            {
                lista = _productService.GetAll();
            }
            else
            {
                var idSupplier = User.Identity.GetUserId();
                lista = _productService.GetAllBySupplier(idSupplier);
            }

            return View(lista);
        }

        #region Insert

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Insert(ProductViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    FlashMessage.Warning("Formulário incompleto");
                    return View(nameof(Insert), viewModel);
                }

                // store Id é o ID do Fornecedor logado
                var storeId = User.Identity.GetUserId();

                var product = new Product
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    BoxDepth = Convert.ToDecimal(viewModel.BoxDepth),
                    BoxWidth = Convert.ToDecimal(viewModel.BoxWidth),
                    BoxHeight = Convert.ToDecimal(viewModel.BoxHeight),
                    Price = Convert.ToDecimal(viewModel.Price.Replace(".", string.Empty).Replace(",", ".")),
                    Brand = viewModel.Brand,
                    Category = viewModel.Category,
                    Store = storeId,
                    Unit = Convert.ToDecimal(viewModel.Unit),
                    Weight = Convert.ToDecimal(viewModel.Weight),
                    Measure = viewModel.Measure
                };

                _productService.Insert(product);
                FlashMessage.Success("Cadastrado com sucesso");

                return RedirectToAction(nameof(Index), "Product");
            }
            catch (Exception e)
            {
                FlashMessage.Error(e.Message);
                return View(nameof(Insert), viewModel);
            }

        }

        #endregion

        #region Update

        public ActionResult Update(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel viewModel)
        {
            return RedirectToAction(nameof(Index), "Product");
        }

        #endregion
    }
}