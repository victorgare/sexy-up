using SexyUp.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface IWishListRepository
    {
        void Insert(WishList wishList);
        List<WishList> Where(Expression<Func<WishList, bool>> predicate);
        WishList Find(Expression<Func<WishList, bool>> predicate);
        void Delete(WishList wishList);
    }
}