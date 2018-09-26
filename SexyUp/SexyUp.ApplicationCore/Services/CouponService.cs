using System;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;

namespace SexyUp.ApplicationCore.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public void Insert(Coupon coupon)
        {
            coupon.StartDate = DateTime.Now;
            _couponRepository.Insert(coupon);
        }

        public Coupon FindByName(string couponName)
        {
            return _couponRepository.Find(c => c.Name.ToLower().Equals(couponName.ToLower()));
        }
    }
}