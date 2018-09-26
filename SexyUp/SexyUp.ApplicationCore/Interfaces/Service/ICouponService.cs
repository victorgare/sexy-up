using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface ICouponService
    {
        void Insert(Coupon coupon);
        Coupon FindByName(string couponName);
    }
}