using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Models;
using WajedApi.Serveries.MarketsService;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("market")]
    public class MarketsController : ControllerBase
    {
           private readonly  IMarketsService _repository;
        private IMapper _mapper;
        public MarketsController( IMarketsService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("get-marketDetails")]
        public async Task<ActionResult> GetMarketDetails([FromQuery] int marketId,[FromQuery] string userId)
        {
           

           

            return  Ok(await _repository.GetMarketDetails(marketId,userId));
        }


         [HttpPost]
        [Route("delete-market")]
        public async Task<ActionResult> DeleteMarket([FromForm] int marketId)
        {
           

           

            return  Ok(await _repository.DeleteAsync(marketId));
        }



        [HttpGet]
        [Route("get-markets-byFieldId")]
        public async Task<ActionResult> GetMarketsByFieldId([FromQuery] int fieldId,[FromQuery]int  AddressId,[FromQuery]int  page)
        {
           

           

            return  Ok(await _repository.GetMarketsByFieldId(fieldId,AddressId,page));
        }




        [HttpPost]
        [Route("add-Market")]
        public async Task<ActionResult> AddMarket([FromForm] Market market)
        {
            if (market == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(market);

            return Ok(market);
        }


         [HttpPost]
        [Route("search-Market")]
        public async Task<ActionResult> SearchMarket([FromForm] string  textSearch,[FromForm]int AddressId)
        {
        
            return Ok(await _repository.SearchMarket(textSearch,AddressId));
        }

    }
}