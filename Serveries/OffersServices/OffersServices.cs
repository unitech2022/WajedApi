using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using X.PagedList;

namespace WajedApi.Serveries.OffersServices
{
    public class OffersServices : IOffersServices 
    {

           private readonly AppDBcontext _context;
        private IMapper _mapper;

        public OffersServices(AppDBcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<dynamic> AddAsync(dynamic offer)
        {
            await _context.Offers!.AddAsync(offer);

            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {
           Offer? offer = await _context.Offers!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (offer != null)
            {
                _context.Offers!.Remove(offer);

                await _context.SaveChangesAsync();
            }

            return offer!;
        }

        public async Task<dynamic> GetItems(string UserId, int page)
        {
             List<Order> Orders = await _context.Orders!.Where(i => i.UserId==UserId && i.Status==0).ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(Orders.Count() / pageResults);

            var items = await Orders
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

        public async Task<dynamic> GitById(int typeId)
        {
           Offer? offer = await _context.Offers!.FirstOrDefaultAsync(x => x.Id == typeId);
            return offer!;
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);
        }
        

        public void UpdateObject(dynamic category)
        {
           // no thing
        }
    }
}