using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Models;
using WajedApi.Serveries.CouponService;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("coupon")]
    public class CouponsController : ControllerBase
    {
         private readonly ICouponService _repository;
        private IMapper _mapper;
        public CouponsController(ICouponService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

         [HttpPost]
        [Route("add-coupon")]
        public async Task<ActionResult> AddCoupon([FromForm] Coupon coupon)
        {
            if (coupon == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(coupon);

            return Ok(coupon);
        }


        [HttpGet]
        [Route("get-coupons")]
        public async Task<ActionResult> GetCoupons()
        {

            return Ok(await _repository.GetCoupons());
        }

         
        [HttpGet]
        [Route("get-coupon-byId")]
        public async Task<ActionResult> GetCouponById([FromQuery] int couponId)
        {

            return Ok(await _repository.GitById(couponId));
        }



          [HttpPost]
        [Route("delete-coupon")]
        public async Task<ActionResult> DeleteCoupon([FromForm] int couponId)
        {
            Coupon coupon = await _repository.DeleteAsync(couponId);

            if (coupon == null)
            {

                return NotFound();
            }



            return Ok(coupon);
        }




    }
}













