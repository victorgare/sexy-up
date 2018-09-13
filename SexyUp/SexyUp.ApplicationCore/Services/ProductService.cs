using System;
using System.Collections.Generic;
using SexyUp.ApplicationCore.Constants;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;

namespace SexyUp.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Insert(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            product.ProductStatus = ProductStatus.Ativo;

            _productRepository.Insert(product);
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

        public List<Product> SearchTerm(string termToSearch, int page, int pageItens, out int totalItens)
        {
            // quebra a string nos espacos para pesquisar os produtos por palavra chave
            var splitedTerms = termToSearch.ToLower().Split(null);

            return _productRepository.SearchTerms(splitedTerms, page, pageItens, out totalItens);
        }
    }
}