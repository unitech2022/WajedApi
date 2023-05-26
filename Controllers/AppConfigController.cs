using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.AppConfigServices;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("appConfig")]
    public class AppConfigController : ControllerBase
    {
        
           private readonly IAppConfigServices _repository;
        private IMapper _mapper;
        public AppConfigController(IAppConfigServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


[HttpPost]
        [Route("add-AppConfig")]
        public async Task<ActionResult> AddAppConfig([FromForm] AppConfig AppConfig)
        {
            if (AppConfig == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(AppConfig);

            return Ok(AppConfig);
        }


        [HttpGet]
        [Route("get-AppConfigs")]
        public async Task<ActionResult> GetAppConfigs([FromQuery] string UserId,[FromQuery]int  page)
        {

            return Ok(await _repository.GetItems(UserId,page));
        }




         [HttpPut]
        [Route("update-AppConfig")]
        public async Task<ActionResult> UpdateAppConfig([FromForm] UpdateAppConfigDto UpdateAppConfig, [FromForm] int id)

        {
            AppConfig AppConfig = await _repository.GitById(id);
            if (AppConfig == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateAppConfig, AppConfig);

            _repository.UpdateObject(AppConfig);
            _repository.SaveChanges();

            return Ok(AppConfig);
        }

        [HttpPost]
        [Route("delete-AppConfig")]
        public async Task<ActionResult> DeleteAppConfig([FromForm] int AppConfigId)
        {
            AppConfig AppConfig = await _repository.DeleteAsync(AppConfigId);

            if (AppConfig == null)
            {

                return NotFound();
            }



            return Ok(AppConfig);
        }

  


    }
}