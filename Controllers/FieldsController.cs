using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Dtos;
using WajedApi.Models;
using WajedApi.Serveries.FieldService;

namespace WajedApi.Controllers
{
    [ApiController]
    [Route("field")]
    public class FieldsController : ControllerBase
    {
        

        private readonly IFieldService _repository;
        private IMapper _mapper;

        public FieldsController(IFieldService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-Field")]
        public async Task<ActionResult> AddField([FromForm] Field Field)
        {
            if (Field == null)
            {
                return NotFound();
            }

            await _repository.AddField(Field);

            return Ok(Field);
        }


        [HttpGet]
        [Route("get-fields")]
        public async Task<ActionResult> GetFields([FromQuery] string UserId,[FromQuery]int  page)
        {

            return Ok(await _repository.GetFields(UserId,page));
        }




         [HttpPut]
        [Route("update-Field")]
        public async Task<ActionResult> UpdateField([FromForm] UpdateFieldDto UpdateField, [FromForm] int id)

        {
            Field field = await _repository.GitFieldById(id);
            if (field == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateField, field);

            _repository.UpdateField(field);
            _repository.SaveChanges();

            return Ok(field);
        }

        [HttpPost]
        [Route("delete-Field")]
        public async Task<ActionResult> DeleteField([FromForm] int FieldId)
        {
            Field field = await _repository.DeleteField(FieldId);

            if (field == null)
            {

                return NotFound();
            }



            return Ok(field);
        }

    }
    
}