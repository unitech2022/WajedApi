using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using Microsoft.AspNetCore.Authorization;
using WajedApi.Serveries.CartsService;

namespace WajedApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("cart")]
    public class CartsController : ControllerBase
    {

        private readonly ICartsService _repository;
        
        private IMapper _mapper;
        public CartsController(ICartsService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-cart")]
        public async Task<ActionResult> Addcart([FromForm] Cart cart)
        {
            if (cart == null)
            {
                return NotFound();
            }

            await _repository.AddCart(cart);

            return Ok(cart);
        }


        [HttpGet]
        [Route("get-carts")]
        public async Task<ActionResult> GetCarts([FromQuery] string UserId, [FromQuery] string code,[FromQuery] int AddressId)
        {

            return Ok(await _repository.GetCarts(UserId, code,AddressId));
        }




        [HttpPut]
        [Route("update-cart")]
        public async Task<ActionResult> UpdateCart([FromForm] Cart updateCart)

        {

             Cart cart = await _repository.GitCartById(updateCart.Id);

             if(cart ==null){
                return NotFound();
             }


            return Ok(await _repository.UpdateCart(updateCart));

            
        }
        

        [HttpPost]
        [Route("delete-cart")]
        public async Task<ActionResult> DeleteCart([FromForm] int cartId)
        {
            Cart cart = await _repository.DeleteCart(cartId);

            if (cart == null)
            {

                return NotFound();
            }



            return Ok(cart);
        }



    }
}