using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WajedApi.Models;
using WajedApi.Serveries.AddressesServices;

namespace WajedApi.Controllers
{
    [Route("rates")]
    public class RatesController : Controller
    {

        private readonly IRateServices _repository;
        private IMapper _mapper;

        public RatesController(IRateServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add-rate")]
        public async Task<ActionResult> AddRateMarket([FromForm] Rate rate)
        {

            return Ok(await _repository.AddRate(rate));

        }

    }
}