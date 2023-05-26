using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.AlertsServices;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("alerts")]
    public class AlertsController : ControllerBase
    {
          private readonly IAlertsServices _repository;
        private IMapper _mapper;
        public AlertsController(IAlertsServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-Alert")]
        public async Task<ActionResult> AddAlert([FromForm] Alert Alert)
        {
            if (Alert == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(Alert);

            return Ok(Alert);
        }


        [HttpGet]
        [Route("get-Alerts")]
        public async Task<ActionResult> GetAlerts([FromQuery] string UserId,[FromQuery]int  page)
        {

            return Ok(await _repository.GetItems(UserId,page));
        }




         [HttpPut]
        [Route("update-Alert")]
        public async Task<ActionResult> UpdateAlert([FromForm] UpdateAlertDto UpdateAlert, [FromForm] int id)

        {
            Alert Alert = await _repository.GitById(id);
            if (Alert == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateAlert, Alert);

            _repository.UpdateObject(Alert);
            _repository.SaveChanges();

            return Ok(Alert);
        }

        [HttpPost]
        [Route("delete-Alert")]
        public async Task<ActionResult> DeleteAlert([FromForm] int AlertId)
        {
            Alert Alert = await _repository.DeleteAsync(AlertId);

            if (Alert == null)
            {

                return NotFound();
            }



            return Ok(Alert);
        }

    }
}