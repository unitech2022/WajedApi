using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Models.BaseEntity;
using X.PagedList;

namespace WajedApi.Serveries.DashboardService
{
    public class DashboardService : IDashboardService
    {

       
         private readonly IMapper _mapper;

       
        private readonly AppDBcontext _context;

        public DashboardService(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<object> GetHome()
        {
            var orders=await _context.Orders!.ToListAsync();
            var products=await _context.Products!.ToListAsync();
            var users=await _context.Users!.ToListAsync();
            var markets=await _context.Markets!.ToListAsync();

            return new{
              orders=orders.Count,
              products =products.Count,
              users=users.Count,
              markets=markets.Count
            };

        }

        public async Task<object> GetMarkets(int page)
        {
            var markets=await _context.Markets!.ToListAsync();

              var pageResults = 10f;
            var pageCount = Math.Ceiling(markets.Count() / pageResults);

            var items = await markets
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

                 BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
            
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}