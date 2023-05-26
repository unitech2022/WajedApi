using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models;

namespace WajedApi.Serveries.CouponService
{
    public interface ICouponService : BaseInterface
    {
        Task<List<Coupon>> GetCoupons();
    }
}