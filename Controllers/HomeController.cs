using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Serveries.HomeService;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
         private readonly IHomeService _repository;
        private IMapper _mapper;

        public HomeController(IHomeService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-home-data")]
        public async Task<ActionResult> GetFields([FromQuery] string UserId,[FromQuery]int  addressId)
        {

            return Ok(await _repository.GetHomeData(UserId,addressId));
        }


         [HttpGet]
        [Route("get-home-data-provider")]
        public async Task<ActionResult> GetHomeDataProvider([FromQuery] string UserId,[FromQuery]int  page)
        {

            return Ok(await _repository.GetHomeDataProvider(UserId,page));
        }

    }
}