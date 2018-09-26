using Microsoft.AspNet.Identity;
using SexyUp.ApplicationCore.Constants;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Service;
using SexyUp.Web.Libraries.FlashMessage;
using SexyUp.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SexyUp.Web.Controllers
{
    [Authorize(Roles = Roles.AdministradorAndFornecedor)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, ICategoryService categoryService, IImageService imageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _imageService = imageService;
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
            var viewModel = CreateViewMoldel();
            return View(viewModel);
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
                    CategoryId = viewModel.Category,
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

        #region Edit

        public ActionResult Edit(string id)
        {
            var product = _productService.GetById(id);

            var viewModel = CreateViewMoldel();

            viewModel.Id = product.Id;
            viewModel.Name = product.Name;
            viewModel.Description = product.Description;
            viewModel.Price = product.Price.ToString(CultureInfo.CurrentCulture);
            viewModel.Weight = product.BoxHeight.ToString();
            viewModel.BoxHeight = product.BoxHeight.ToString();
            viewModel.BoxWidth = product.BoxWidth.ToString();
            viewModel.BoxDepth = product.BoxDepth.ToString();
            viewModel.Unit = product.Unit.ToString();
            viewModel.Measure = product.Measure;
            viewModel.Brand = product.Brand;
            viewModel.Category = product.CategoryId;
            viewModel.Images = product.Image;



            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    FlashMessage.Warning("Formulário incompleto");
                    return View(nameof(Edit), viewModel);
                }


                var product = new Product
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    BoxDepth = Convert.ToDecimal(viewModel.BoxDepth),
                    BoxWidth = Convert.ToDecimal(viewModel.BoxWidth),
                    BoxHeight = Convert.ToDecimal(viewModel.BoxHeight),
                    Price = Convert.ToDecimal(viewModel.Price.Replace(".", string.Empty).Replace(",", ".")),
                    Brand = viewModel.Brand,
                    CategoryId = viewModel.Category,
                    Unit = Convert.ToDecimal(viewModel.Unit),
                    Weight = Convert.ToDecimal(viewModel.Weight),
                    Measure = viewModel.Measure
                };

                _productService.Update(product);
                FlashMessage.Success("Alterado com sucesso");

                return RedirectToAction(nameof(Index), "Product");
            }
            catch (Exception e)
            {
                FlashMessage.Error(e.Message);
                return View(nameof(Edit), viewModel);
            }
        }

        #endregion

        #region Private Methods

        private ProductViewModel CreateViewMoldel()
        {
            var categories = _categoryService.GetAll();
            return new ProductViewModel
            {
                CategorySelectList = new SelectList(categories, nameof(Category.Id), nameof(Category.Name))
            };
        }
        #endregion

        #region IMAGE

        //[HttpPost]
        public ActionResult ProductImages(string productId)
        {
            var images = _imageService.GetByProductId(productId);
            return PartialView("_Partials/ProductImages", images);
        }

        [HttpPost]
        public JsonResult DeleteImage(string imageId)
        {
            var image = _imageService.GetById(imageId);
            return Json(new
            {
                success = true,
                message = string.Empty
            });
        }
        #endregion

        #region MassInsert

        public ActionResult MassInsert()
        {
            return View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult MassInsert(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                {
                    FlashMessage.Error("Aconteceu um erro ao subir o arquivo");
                    return RedirectToAction(nameof(MassInsert), "Product");
                }

                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension?.ToLower() != ".xlsx")
                {
                    FlashMessage.Warning("Arquivo inválido");
                    return RedirectToAction(nameof(MassInsert), "Product");
                }

                var fornecedorId = User.Identity.GetUserId();
                var fileName = $"{Guid.NewGuid().ToString()}_{fornecedorId}.xlsx";


                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);

                _productService.MassInsert(path, fornecedorId);

                // apaga o arquivo
                new FileInfo(path).Delete();

                FlashMessage.Success("Importado com sucesso");
                return RedirectToAction(nameof(Index), "Product");

            }
            catch (Exception e)
            {
                FlashMessage.Error(e.Message);
                return RedirectToAction(nameof(MassInsert), "Product");
            }

            return View();
        }

        public FileResult DefaultWorksheet()
        {
            var file = Path.Combine(Server.MapPath("~/App_Data"), "ProductDefaultWorksheet.xlsx");
            return File(file, MimeTypes.GetMimeType(file), "Produtos.xlsx");
        }
        #endregion
    }
}