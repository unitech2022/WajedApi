using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WajedApi.Serveries.DashboardService;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("dash")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _repository;
        private IMapper _mapper;

        public DashboardController(IDashboardService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-home")]
        public async Task<ActionResult> GetFields()
        {

            return Ok(await _repository.GetHome());
        }

        [HttpGet]
        [Route("get-markets")]
        public async Task<ActionResult> GeMarkets([FromQuery] int page)
        {
            return Ok(await _repository.GetMarkets(page));
        }


    }
}