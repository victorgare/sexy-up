using SexyUp.ApplicationCore.Entities;
using System.Collections.Generic;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface IWishListService
    {
        void Insert(WishList wishList);
        List<WishList> GetUsersWishList(string userId);
        void Delete(string id);
    }
}