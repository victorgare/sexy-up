using System;
using System.Linq;
using System.Linq.Expressions;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;

namespace SexyUp.Infrastructure.Repository
{
    public class CouponRepository : ICouponRepository
    {
        public void Insert(Coupon coupon)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Coupon.Add(coupon);
                context.SaveChanges();
            }
        }

        public Coupon Find(Expression<Func<Coupon, bool>> predicate)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Coupon.FirstOrDefault(predicate);
            }
        }
    }
}