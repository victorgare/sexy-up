using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface IProductService
    {
        void Insert(Product product);
        void MassInsert(string filePath, string supplierId);
        void Update(Product product);
        Product GetById(string id);
        List<Product> GetAll();
        List<Product> GetAllBySupplier(string idSupplier);
        List<Product> SearchTerm(string termToSearch, string[] categories, decimal? minValue, decimal? maxValue, int page, int pageItens, out int totalItens);
        List<Category> GetAllCategoriesBySeachTerm(string termToSearch);
        List<decimal> GetAllPricesBySearchTerm(string termToSearch);
    }
}