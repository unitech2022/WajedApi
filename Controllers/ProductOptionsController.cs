using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.ProductsOptionsServices;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("productOptions")]
    public class ProductOptionsController : ControllerBase
    {
        


          private readonly IProductsOptionsServices _repository;
        private IMapper _mapper;

        public ProductOptionsController(IProductsOptionsServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



           [HttpPost]
        [Route("add-Product-options")]
        public async Task<ActionResult> AddProduct([FromForm] ProductsOption Product)
        {
            if (Product == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(Product);

            return Ok(Product);
        }


        [HttpGet]
        [Route("get-Products-options")]
        public async Task<ActionResult> GetProducts([FromQuery] string UserId, [FromQuery] int page)
        {

            return Ok(await _repository.GetItems(UserId, page));
        }


        [HttpGet]
        [Route("get-Product-options-byId")]
        public async Task<ActionResult> GetProductById([FromQuery] int productOptionId)
        {

            return Ok(await _repository.GitById(productOptionId));
        }


        [HttpGet]
        [Route("get-Products-options-By-prId")]
        public async Task<ActionResult> GetProductOptionsByProductId([FromQuery] string UserId, [FromQuery] int page, [FromQuery] int productId)
        {

            return Ok(await _repository.GitProductOptionsByProductId(UserId, productId, page));
        }



        [HttpPut]
        [Route("update-Product-options")]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductOptionsDto UpdateProduct, [FromForm] int id)

        {
            ProductsOption productsOption = await _repository.GitById(id);
            if (productsOption == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateProduct, productsOption);

            _repository.UpdateObject(productsOption);
            _repository.SaveChanges();

            return Ok(productsOption);
        }

        [HttpPost]
        [Route("delete-Product-options")]
        public async Task<ActionResult> DeleteProduct([FromForm] int produceOptionId)
        {
            ProductsOption productOptions = await _repository.DeleteAsync(produceOptionId);

            if (productOptions == null)
            {

                return NotFound();
            }



            return Ok(productOptions);
        }




    }
}