using System;
using System.Collections.Generic;
using System.Linq;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;

namespace SexyUp.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAll()
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Category.ToList();
            }
        }
    }
}