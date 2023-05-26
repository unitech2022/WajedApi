using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Data;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace WajedApi.Serveries.OrderItems
{
    public class OrderItemsServices : IOrderItemsServices
    {

            private readonly IMapper _mapper;

       
        private readonly AppDBcontext _context;

        public OrderItemsServices(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
           await _context.OrderItems!.AddAsync(orderItem);

            await _context.SaveChangesAsync();

            return orderItem;
        }

       

        public async Task<BaseResponse> GetOrderItems(string UserId, int page)
        {
            List<OrderItem> categories = await _context.OrderItems!.Where(t=> t.UserId==UserId).ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(categories.Count() / pageResults);

            var items = await categories
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


        
        public async Task<BaseResponse> GitOrderItemByOrderId(string UserId,int orderId,int page )
        {
            List<OrderItem> categories = await _context.OrderItems!.Where(i => i.ProductId==orderId ).ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(categories.Count() / pageResults);

            var items = await categories
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






        public  async Task<OrderItem> GitOrderItemById(int OrderItemId)
        {
            OrderItem? orderItem = await _context.OrderItems!.FirstOrDefaultAsync(x => x.Id == OrderItemId);
            return orderItem!;
        }

        public bool SaveChanges()
        {
              return (_context.SaveChanges() >= 0);
        }

        public void UpdateOrderItem(OrderItem OrderItem)
        {
           // no thing 
        }


         public async Task<OrderItem> DeleteOrderItem(int orderItemId)
        {
             OrderItem? OrderItem = await _context.OrderItems!.FirstOrDefaultAsync(x => x.Id == orderItemId);

            if (OrderItem != null)
            {
                _context.OrderItems!.Remove(OrderItem);

                await _context.SaveChangesAsync();
            }

            return OrderItem!;
        }
    }
}