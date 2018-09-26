using System;
using System.Linq.Expressions;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface ICouponRepository
    {
        void Insert(Coupon coupon);
        Coupon Find(Expression<Func<Coupon, bool>> predicate);
    }
}