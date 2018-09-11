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
        List<Product> SearchTerms(string[] terms);
    }
}