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

        public List<Product> SearchTerms(string[] terms, string[] categories, decimal? minValue, decimal? maxValue, int page, int pageItens, out int totalItens)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                var whereClause = SearchTermPredicate(terms, categories, minValue, maxValue);

                totalItens = context.Product.AsExpandable().Where(whereClause).Distinct().Count();

                return context.Product
                                        .AsExpandable()
                                        .Where(whereClause)
                                        .Distinct()
                                        .Include(c => c.Category)
                                        .Include(c => c.Image)
                                        .OrderBy(c => c.Id)
                                        .Skip(page * pageItens)
                                        .Take(pageItens)
                                        .ToList();
            }
        }

        public List<Category> GetAllCategoriesBySeachTerm(string[] terms)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                var whereClause = SearchTermPredicate(terms, null, null, null);

                return context.Product
                    .AsExpandable()
                    .Where(whereClause)
                    .Distinct()
                    .Include(c => c.Category)
                    .Select(c => c.Category)
                    .Distinct()
                    .ToList();
            }
        }

        public List<decimal> GetAllPricesBySearchTerm(string[] terms)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                var whereClause = SearchTermPredicate(terms, null, null, null);

                // pega o maior preco dos produtos encontrados
                var maxPrice = Convert.ToDecimal(context.Product
                    .AsExpandable()
                    .Where(whereClause)
                    .Distinct()
                    .Select(c => c.Price)
                    .Distinct()
                    .Max());

                // pega o menor preco dos produtos encontrados
                var minPrice = Convert.ToDecimal(context.Product
                    .AsExpandable()
                    .Where(whereClause)
                    .Distinct()
                    .Select(c => c.Price)
                    .Distinct()
                    .Min());

                return new List<decimal>
                {
                    minPrice,
                    maxPrice
                };
            }
        }

        private Expression<Func<Product, bool>> SearchTermPredicate(string[] searchValue, string[] categories, decimal? minValue, decimal? maxValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<Product>(c => c.ProductStatus == ProductStatus.Ativo);


            predicate = predicate.And(s => searchValue.Any(srch => s.Name.ToLower().Contains(srch))
                                                        || searchValue.Any(srch => s.Description.ToLower().Contains(srch))
                                                        || searchValue.Any(srch => s.Brand.ToLower().Contains(srch)));


            if (categories != null)
            {
                predicate.And(c => categories.Any(s => c.CategoryId.Contains(s)));
            }

            if (minValue != null)
            {
                predicate.And(c => c.Price >= minValue);
            }

            if (maxValue != null)
            {
                predicate.And(c => c.Price <= maxValue);
            }

            return predicate;
        }
    }
}