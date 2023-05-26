using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.OffersServices;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("offer")]
    public class OffersController : ControllerBase
    {
          private readonly IOffersServices _repository;
        private IMapper _mapper;
        public OffersController(IOffersServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
       

        [HttpPost]
        [Route("add-Offer")]
        public async Task<ActionResult> AddOffer([FromForm] Offer Offer)
        {
            if (Offer == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(Offer);

            return Ok(Offer);
        }


        [HttpGet]
        [Route("get-Offers")]
        public async Task<ActionResult> GetOffers([FromQuery] string UserId,[FromQuery]int  page)
        {

            return Ok(await _repository.GetItems(UserId,page));
        }




         [HttpPut]
        [Route("update-Offer")]
        public async Task<ActionResult> UpdateOffer([FromForm] UpdateOfferDto OfferUpdate,[FromForm] int id){


             Offer offer = await _repository.GitById(id);
            if (offer == null)
            {
                return NotFound();
            }
            _mapper.Map(OfferUpdate, offer);

            _repository.UpdateObject(offer);
            _repository.SaveChanges();

            return Ok(offer);
        }
    
        [HttpPost]
        [Route("delete-Offer")]
        public async Task<ActionResult> DeleteOffer([FromForm] int OfferId)
        {
            Offer Offer = await _repository.DeleteAsync(OfferId);

            if (Offer == null)
            {

                return NotFound();
            }



            return Ok(Offer);
        }





    }
}