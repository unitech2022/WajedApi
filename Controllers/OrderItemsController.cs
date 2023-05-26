using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.OrderItems;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("orderItem")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsServices _repository;
        private IMapper _mapper;
        public OrderItemsController(IOrderItemsServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("add-OrderItem")]
        public async Task<ActionResult> AddOrderItem([FromForm] OrderItem OrderItem)
        {
            if (OrderItem == null)
            {
                return NotFound();
            }

            await _repository.AddOrderItem(OrderItem);

            return Ok(OrderItem);
        }


        [HttpGet]
        [Route("get-OrderItems")]
        public async Task<ActionResult> GetOrderItems([FromQuery] string UserId, [FromQuery] int page)
        {

            return Ok(await _repository.GetOrderItems(UserId, page));
        }


        [HttpGet]
        [Route("get-OrderItems-byId")]
        public async Task<ActionResult> GetOrderItemsById([FromQuery] string UserId, [FromQuery] int page, [FromQuery] int orderId)
        {

            return Ok(await _repository.GitOrderItemByOrderId(UserId, orderId, page));
        }


         [HttpPut]
        [Route("update-OrderItem")]
        public async Task<ActionResult> UpdateOrderItem([FromForm] UpdateOrderItemDto UpdateOrderItem, [FromForm] int id)

        {
            OrderItem OrderItem = await _repository.GitOrderItemById(id);
            if (OrderItem == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateOrderItem, OrderItem);

            _repository.UpdateOrderItem(OrderItem);
            _repository.SaveChanges();

            return Ok(OrderItem);
        }

        [HttpPost]
        [Route("delete-OrderItem")]
        public async Task<ActionResult> DeleteOrderItem([FromForm] int OrderItemId)
        {
            OrderItem OrderItem = await _repository.DeleteOrderItem(OrderItemId);

            if (OrderItem == null)
            {

                return NotFound();
            }



            return Ok(OrderItem);
        }




    }
}