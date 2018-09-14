using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface IProductRepository
    {
        void Insert(Product product);
        void Update(Product product);
        Product GetById(string id);
        List<Product> GetAll();
        List<Product> GetAllBySupplier(string idSupplier);
        List<Product> SearchTerms(string[] terms, string[] categories, decimal? minValue, decimal? maxValue, int page, int pageItens, out int totalItens);
        List<Category> GetAllCategoriesBySeachTerm(string[] terms);
        List<decimal> GetAllPricesBySearchTerm(string[] terms);
    }
}