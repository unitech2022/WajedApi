using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;

namespace WajedApi.Serveries.OrderItems
{
    public interface IOrderItemsServices
    {
        
          Task<BaseResponse> GetOrderItems(string UserId,int page);

         Task<BaseResponse> GitOrderItemByOrderId(string UserId,int orderId,int page );

         Task<OrderItem> AddOrderItem(OrderItem orderItem);

         Task<OrderItem> GitOrderItemById(int OrderItemId);


         Task<OrderItem> DeleteOrderItem(int OrderItemId);

        void UpdateOrderItem(OrderItem Cart);


         bool SaveChanges(); 
    }
}