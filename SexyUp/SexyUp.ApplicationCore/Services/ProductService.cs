using System;
using SexyUp.ApplicationCore.Constants;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SexyUp.Utils.Utils;

namespace SexyUp.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;

        public ProductService(IProductRepository productRepository, IImageService imageService, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
            _categoryService = categoryService;
        }

        public void Insert(Product product)
        {
            product.ProductStatus = ProductStatus.Ativo;

            _productRepository.Insert(product);
        }

        public void MassInsert(string filePath, string supplierId)
        {
            var dataTable = ExcelReader.ConvertExcelToDataTable(filePath);
            var listaCategorias = _categoryService.GetAll();

            var listaCadastrar = new List<Product>();

            foreach (DataRow row in dataTable.Rows)
            {
                var name = row["Nome"].ToString();
                var descricao = row["Descrição"].ToString();
                var price = Convert.ToDecimal(row["Preço"].ToString());
                var category = row["Categoria"].ToString();
                var weight = row["Peso"].ToString();
                var boxHeight = row["Altura da caixa"].ToString();
                var boxWidth = row["Largura da caixa"].ToString();
                var boxDepth = row["Profundidade da caixa"].ToString();
                var unit = row["Unidades"].ToString();
                var unitOfMeasure = row["Unidade de medida"].ToString();
                var brand = row["Marca"].ToString();

                var product = new Product
                {
                    Name = name,
                    Description = descricao,
                    Price = price,
                    CategoryId = listaCategorias.FirstOrDefault(c => c.Name.ToLower().Equals(category.ToLower()))?.Id,
                    Weight = string.IsNullOrWhiteSpace(weight) ? (decimal?)null : Convert.ToDecimal(weight),
                    BoxHeight = string.IsNullOrWhiteSpace(boxHeight) ? (decimal?)null : Convert.ToDecimal(boxHeight),
                    BoxWidth = string.IsNullOrWhiteSpace(boxWidth) ? (decimal?)null : Convert.ToDecimal(boxWidth),
                    BoxDepth = string.IsNullOrWhiteSpace(boxDepth) ? (decimal?)null : Convert.ToDecimal(boxDepth),
                    Unit = string.IsNullOrWhiteSpace(unit) ? (decimal?)null : Convert.ToDecimal(unit),
                    Measure = unitOfMeasure,
                    Brand = brand,
                    Store = supplierId
                };

                if (!product.IsValid)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var erro in product.Errors)
                    {
                        stringBuilder.Append($"{erro} {Environment.NewLine}");
                    }

                    throw new Exception(stringBuilder.ToString());
                }

                // insere o produto
                Insert(product);
            }
        }

        public void Update(Product product)
        {
            // Quando atualizamos um produto, nos atualizamos o antio para bloqueado e criamos um novo
            // assim podemos manter o historico de alteracoees e variacoes de preco

            var newProduct = new Product
            {
                Name = product.Name,
                ProductStatus = ProductStatus.Bloqueado,
                BoxDepth = product.BoxDepth,
                BoxHeight = product.BoxHeight,
                BoxWidth = product.BoxWidth,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Store = product.Store,
                Unit = product.Unit,
                Weight = product.Weight,
                Measure = product.Measure
            };


            // recupera o produto antigo e atualiza ele pra atualizado
            var oldProduct = GetById(product.Id);
            oldProduct.ProductStatus = ProductStatus.Inativo;
            _productRepository.Update(oldProduct);

            // cria o novo produto atualizado
            _productRepository.Insert(newProduct);

            oldProduct.Image.ForEach(c => c.IdProduct = newProduct.Id);
            // cadastra as imagens do produto desatualizado no novo
            _imageService.Insert(oldProduct.Image);
        }

        public Product GetById(string id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public List<Product> GetAllBySupplier(string idSupplier)
        {
            return _productRepository.GetAllBySupplier(idSupplier);
        }

        public List<Product> SearchTerm(string termToSearch, string[] categories, decimal? minValue, decimal? maxValue, int page, int pageItens, out int totalItens)
        {
            // quebra a string nos espacos para pesquisar os produtos por palavra chave
            var splitedTerms = termToSearch.ToLower().Split(null);

            return _productRepository.SearchTerms(splitedTerms, categories, minValue, maxValue, page, pageItens, out totalItens);
        }

        public List<Category> GetAllCategoriesBySeachTerm(string termToSearch)
        {
            // quebra a string nos espacos para pesquisar os produtos por palavra chave
            var splitedTerms = termToSearch.ToLower().Split(null);

            return _productRepository.GetAllCategoriesBySeachTerm(splitedTerms);
        }

        public List<decimal> GetAllPricesBySearchTerm(string termToSearch)
        {
            // quebra a string nos espacos para pesquisar os produtos por palavra chave
            var splitedTerms = termToSearch.ToLower().Split(null);

            return _productRepository.GetAllPricesBySearchTerm(splitedTerms);
        }
    }
}