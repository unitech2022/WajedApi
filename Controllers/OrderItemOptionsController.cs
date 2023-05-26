using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.OrderItemOptionsServices;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("OrderItemOption")]
    public class OrderItemOptionController : ControllerBase
    {
        

          private readonly IOrderItemOptionsServices _repository;
        private IMapper _mapper;

        public OrderItemOptionController(IOrderItemOptionsServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



           [HttpPost]
        [Route("add-OrderItemOption")]
        public async Task<ActionResult> AddProduct([FromForm] OrderItemOption orderItemOption)
        {
            if (orderItemOption == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(orderItemOption);

            return Ok(orderItemOption);
        }


        [HttpGet]
        [Route("get-orderItemOptions")]
        public async Task<ActionResult> GetOrderItemOptions([FromQuery] string UserId, [FromQuery] int page)
        {

            return Ok(await _repository.GetItems(UserId, page));
        }


        [HttpGet]
        [Route("get-OrderItemOption-byId")]
        public async Task<ActionResult> GetOrderItemOptionById([FromQuery] int orderItemOptionId)
        {

            return Ok(await _repository.GitById(orderItemOptionId));
        }


      


         [HttpPut]
        [Route("update-OrderItemOption")]
        public async Task<ActionResult> UpdateOrderItemOption([FromForm] UpdateOrderItemOptionDto orderItemOptionDto, [FromForm] int id)

        {
            OrderItemOption OrderItemOption = await _repository.GitById(id);
            if (OrderItemOption == null)
            {
                return NotFound();
            }
            _mapper.Map(orderItemOptionDto, OrderItemOption);

            _repository.UpdateObject(OrderItemOption);
            _repository.SaveChanges();

            return Ok(OrderItemOption);
        }

        [HttpPost]
        [Route("delete-orderItemOption")]
        public async Task<ActionResult> DeleteOrderItemOptionDto([FromForm] int produceOptionId)
        {
            OrderItemOption productOptions = await _repository.DeleteAsync(produceOptionId);

            if (productOptions == null)
            {

                return NotFound();
            }



            return Ok(productOptions);
        }

    }
}