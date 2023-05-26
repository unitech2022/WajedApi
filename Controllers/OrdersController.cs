using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WajedApi.Serveries.OrdersServices;
using AutoMapper;
using WajedApi.Models;
using WajedApi.Dtos;

namespace WajedApi.Controllers
{


    // order status
    // 0 في انتظار التأكيد
    // 1 تم تأكيد طلبك
    // 2 جارى التجهيز 
    // 3 تم التجهيز 
    // 4 جارى التوصيل 
    // 5 تم التسليم 
    [Authorize]
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersServices _repository;
        private IMapper _mapper;
        public OrdersController(IOrdersServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-order")]
        public async Task<ActionResult> AddOrder([FromForm] Order order, [FromForm] int addressId)
        {
            if (order == null)
            {
                return NotFound();
            }

            await _repository.AddOrder(order, addressId);

            return Ok(order);
        }


        [HttpGet]
        [Route("get-Orders")]
        public async Task<ActionResult> GetOrders([FromQuery] string UserId, [FromQuery] int page)
        {

            return Ok(await _repository.GetItems(UserId, page));
        }

        [HttpGet]
        [Route("get-Orders-by-marketId")]
        public async Task<ActionResult> GetOrdersByMarjetId([FromQuery] int marketId)
        {

            return Ok(await _repository.GitOrdersByMarketId(marketId));
        }


        [HttpGet]
        [Route("get-Orders-by-driverId")]
        public async Task<ActionResult> GetOrdersByDriverId([FromQuery] string driverId, [FromQuery] int addressId)
        {

            return Ok(await _repository.GitOrdersByDriverId(driverId, addressId));
        }


        [HttpGet]
        [Route("get-OrderDetails")]
        public async Task<ActionResult> GetOrderDetails([FromQuery] int orderId, [FromQuery] int addressId)
        {

            return Ok(await _repository.GitOrderDetails(orderId, addressId));
        }





        [HttpPut]
        [Route("update-Order")]
        public async Task<ActionResult> UpdateOrder([FromForm] UpdateOrderDto UpdateOrder, [FromForm] int id)

        {
            Order Order = await _repository.GitById(id);
            if (Order == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateOrder, Order);

            // _repository.UpdateObject(Order);
            _repository.SaveChanges();

            return Ok(Order);
        }



        [HttpPut]
        [Route("update-Order-status")]
        public async Task<ActionResult> UpdateOrderStatus([FromForm] int status, [FromForm] int orderId)

        {


            return Ok(await _repository.UpdateOrderStatus(orderId, status));

        }

        [HttpPut]
        [Route("accept-order-driver")]
        public async Task<ActionResult> AcceptOrderDriver([FromForm] string driverId, [FromForm] int orderId)

        {


            return Ok(await _repository.AcceptOrderDriver(orderId, driverId));

        }



        [HttpPost]
        [Route("delete-Order")]
        public async Task<ActionResult> DeleteOrder([FromForm] int OrderId)
        {
            Order Order = await _repository.DeleteAsync(OrderId);

            if (Order == null)
            {

                return NotFound();
            }



            return Ok(Order);
        }

    }
}