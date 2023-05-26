using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.AddressesServices;

namespace WajedApi.Controllers
{
//  dotnet commends

// dotnet publish --configuration Release
// migrations dotnet
// dotnet ef migrations add InitialCreate
 // update database 
// dotnet ef database update
// create
// dotnet new webapi -n name 

    [ApiController]
    [Route("address")]
    public class AddressesController : ControllerBase
    {

        private readonly IAddressesServices _repository;
        private IMapper _mapper;
        public AddressesController(IAddressesServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-address")]
        public async Task<ActionResult> AddAddress([FromForm] Address address)
        {
            if (address == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(address);

            return Ok(address);
        }


        [HttpGet]
        [Route("get-addresses")]
        public async Task<ActionResult> GetAddresses([FromQuery] string UserId, [FromQuery] int page)
        {

            return Ok(await _repository.GetItems(UserId, page));
        }

        [HttpPost]
        [Route("default-address")]
        public async Task<ActionResult> DefaultAddress([FromForm] int addressId, [FromForm] string userId)
        {
            Address address = await _repository.DefaultAddress(addressId, userId);
            if (address == null)
            {

                return NotFound();
            }
            return Ok(address);
        }




        [HttpPut]
        [Route("update-address")]
        public async Task<ActionResult> UpdateAddress([FromForm] UpdateAddressDto UpdateAddress, [FromForm] int id)

        {
            Address address = await _repository.GitById(id);
            if (address == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateAddress, address);

            _repository.UpdateObject(address);
            _repository.SaveChanges();

            return Ok(address);
        }

        [HttpPost]
        [Route("delete-address")]
        public async Task<ActionResult> DeleteAddress([FromForm] int addressId)
        {
            Address address = await _repository.DeleteAsync(addressId);

            if (address == null)
            {

                return NotFound();
            }



            return Ok(address);
        }
    }
}