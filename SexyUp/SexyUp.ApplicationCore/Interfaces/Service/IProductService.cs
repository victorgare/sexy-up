using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface IProductService
    {
        void Insert(Product product);
        void Update(Product product);
        Product GetById(string id);
        List<Product> GetAll();
        List<Product> GetAllBySupplier(string idSupplier);
        List<Product> SearchTerm(string termToSearch);
    }
}