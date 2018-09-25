using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;
using System.Collections.Generic;

namespace SexyUp.ApplicationCore.Services
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepository;

        public WishListService(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        public void Insert(WishList wishList)
        {
            _wishListRepository.Insert(wishList);
        }

        public List<WishList> GetUsersWishList(string userId)
        {
            return _wishListRepository.Where(c => c.UserId.Equals(userId));
        }

        public void Delete(string id)
        {
            var wishList = _wishListRepository.Find(c => c.Id.Equals(id));
            _wishListRepository.Delete(wishList);
        }
    }
}