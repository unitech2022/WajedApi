using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models;

namespace WajedApi.Serveries.OrdersServices
{
    public interface IOrdersServices : BaseInterface
    {

        Task<dynamic> AddOrder(Order order, int AddressId);

        Task<dynamic> GitOrderDetails(int orderId, int AddressId);


          Task<dynamic> GitOrdersByMarketId(int marketId);

          Task<dynamic> GitOrdersByDriverId(string driverId, int AddressId);

        
        Task<dynamic> UpdateOrderStatus(int orderId,int status);

         Task<dynamic> AcceptOrderDriver(int orderId,string driverId);
    }
}