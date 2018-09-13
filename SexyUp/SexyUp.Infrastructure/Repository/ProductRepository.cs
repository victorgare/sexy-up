using System;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
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
                return context.Product
                    .Where(c => c.Store.Equals(idSupplier) && c.ProductStatus != ProductStatus.Bloqueado).ToList();
            }
        }

        public List<Product> SearchTerms(string[] terms, int page, int pageItens, out int totalItens)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                var whereClause = SearchTermPredicate(terms);

                totalItens = context.Product.AsExpandable().Where(whereClause).Distinct().Count();

                return context.Product
                                        .AsExpandable()
                                        .Where(whereClause)
                                        .Distinct()
                                        .OrderBy(c => c.Id)
                                        .Skip(page * pageItens)
                                        .Take(pageItens)
                                        .ToList();
            }
        }

        private Expression<Func<Product, bool>> SearchTermPredicate(string[] searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<Product>(true); // true -where(true) return all

            predicate = predicate.Or(s => searchValue.Any(srch => s.Name.ToLower().Contains(srch)));
            predicate = predicate.Or(s => searchValue.Any(srch => s.Description.ToLower().Contains(srch)));
            predicate = predicate.Or(s => searchValue.Any(srch => s.Brand.ToLower().Contains(srch)));

            return predicate;
        }
    }
}