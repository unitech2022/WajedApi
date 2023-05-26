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

namespace WajedApi.Serveries.OrderItemOptionsServices
{
    public class OrderItemOptionsServices : IOrderItemOptionsServices
    {
        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;

        public OrderItemOptionsServices(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<dynamic> AddAsync(dynamic type)
        {
             await _context.OrderItemOptions!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;
        
        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {
            OrderItemOption? orderItemOption = await _context.OrderItemOptions!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (orderItemOption != null)
            {
                _context.OrderItemOptions!.Remove(orderItemOption);

                await _context.SaveChangesAsync();
            }

            return orderItemOption!;
        }



        public async Task<dynamic> GetItems(string UserId, int page)
        {
             List<OrderItemOption> orderItemOptions = await _context.OrderItemOptions!.ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(orderItemOptions.Count() / pageResults);

            var items = await orderItemOptions
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
             OrderItemOption? orderItemOption = await _context.OrderItemOptions!.FirstOrDefaultAsync(x => x.Id == typeId);
            return orderItemOption!;
        }

        public bool SaveChanges()
        {
             return (_context.SaveChanges() >= 0);
        }

        public  void UpdateObject(dynamic category)
        {
           // nothing 
        }
    }
}