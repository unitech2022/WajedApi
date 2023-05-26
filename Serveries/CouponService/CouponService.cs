using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Models;

namespace WajedApi.Serveries.CouponService
{
    public class CouponService : ICouponService
    {


           private readonly AppDBcontext _context;
        private IMapper _mapper;

        public CouponService(AppDBcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<dynamic> AddAsync(dynamic coupon)
        {
             await _context.Coupons!.AddAsync(coupon);

            await _context.SaveChangesAsync();

            return coupon;
        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {

             Coupon? coupon = await _context.Coupons!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (coupon != null)
            {
                _context.Coupons!.Remove(coupon);

                await _context.SaveChangesAsync();
            }

            return coupon!;
        }

        public async Task<List<Coupon>> GetCoupons()
        {
           return await _context.Coupons!.ToListAsync();
        }

      

        public Task<dynamic> GetItems(string UserId, int page)
        {
            throw new NotImplementedException();
        }

        public async  Task<dynamic> GitById(int typeId)
        {
             Coupon? coupon = await _context.Coupons!.FirstOrDefaultAsync(x => x.Id == typeId);
            return coupon!;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(dynamic category)
        {
            throw new NotImplementedException();
        }
    }
}