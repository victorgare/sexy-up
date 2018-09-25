using LinqKit;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SexyUp.Infrastructure.Repository
{
    public class SexyServiceRepository : ISexyServiceRepository
    {
        public List<SexyService> GetAll()
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.SexyService.ToList();
            }
        }

        public SexyService GetById(string id)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.SexyService.Include(c => c.ApplicationUser).FirstOrDefault(c => c.Id == id);
            }
        }

        public void Insert(SexyService sexyService)
        {
            throw new NotImplementedException();
        }

        public List<SexyService> SearchTerms(string[] terms, int page, int pageItens, out int totalItens)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                var whereClause = SearchTermPredicate(terms);

                totalItens = context.SexyService.AsExpandable().Where(whereClause).Distinct().Count();

                return context.SexyService
                                        .AsExpandable()
                                        .Include(c => c.ApplicationUser)
                                        .Where(whereClause)
                                        .Distinct()
                                        .OrderBy(c => c.Id)
                                        .Skip(page * pageItens)
                                        .Take(pageItens)
                                        .ToList();
            }

        }

        public void Update(SexyService sexyService)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<SexyService, bool>> SearchTermPredicate(string[] searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<SexyService>();


            predicate = predicate.And(s => searchValue.Any(srch => s.NameService.ToLower().Contains(srch))
                                                        || searchValue.Any(srch => s.Description.ToLower().Contains(srch)));

            return predicate;
        }
    }
}
