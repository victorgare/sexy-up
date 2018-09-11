using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SexyUp.ApplicationCore.Constants;

namespace SexyUp.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Insert(Product product)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Product.Add(product);
                context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Product GetById(string id)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Product.Find(id);
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Product.Where(c => c.ProductStatus != ProductStatus.Bloqueado).ToList();
            }
        }

        public List<Product> GetAllBySupplier(string idSupplier)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Product.Where(c => c.Store.Equals(idSupplier) && c.ProductStatus != ProductStatus.Bloqueado).ToList();
            }
        }

        public List<Product> SearchTerms(string[] terms)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                var result = new List<Product>();
                foreach (var term in terms)
                {
                    var itens = context.Product.Where(c => c.Name.Contains(term)
                                                           || c.Description.Contains(term)
                                                           || c.Brand.Contains(term));

                    result.AddRange(itens);
                }

                return result.Distinct().ToList();
            }
        }
    }
}