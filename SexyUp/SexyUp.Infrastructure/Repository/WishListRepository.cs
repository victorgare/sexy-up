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
    public class WishListRepository : IWishListRepository
    {
        public void Insert(WishList wishList)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.WishList.Add(wishList);
                context.SaveChanges();
            }
        }

        public List<WishList> Where(Expression<Func<WishList, bool>> predicate)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.WishList.Where(predicate)
                    .Include(c => c.Product)
                    .Include(c => c.Product.Category)
                    .Include(c => c.Product.Image)
                    .ToList();
            }
        }

        public WishList Find(Expression<Func<WishList, bool>> predicate)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.WishList.FirstOrDefault(predicate);
            }
        }

        public void Delete(WishList wishList)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Entry(wishList).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}