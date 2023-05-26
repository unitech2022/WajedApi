using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using X.PagedList;

namespace WajedApi.Serveries.FieldService
{
    public class FieldService : IFieldService
    {


         private readonly IMapper _mapper;
  private readonly AppDBcontext _context;


       public FieldService(IMapper mapper,  AppDBcontext context)
        {
            _mapper = mapper;
           
            _context = context;
        }


        public async Task<Field> AddField(Field field)
        {
            await _context.Fields!.AddAsync(field);

            await _context.SaveChangesAsync();

            return field;
        }

        public async Task<Field> DeleteField(int FieldId)
        {
            Field? field = await _context.Fields!.FirstOrDefaultAsync(x => x.Id == FieldId);

            if (field != null)
            {
                _context.Fields!.Remove(field);

                await _context.SaveChangesAsync();
            }

            return field!;
        }

        public async Task<BaseResponse> GetFields(string UserId,int page)
        {
             List<Field> fields = await _context.Fields!.ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(fields.Count() / pageResults);

            var items = await fields
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();



            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<Field> GitFieldById(int FieldId)
        {
            Field? field = await _context.Fields!.FirstOrDefaultAsync(x => x.Id == FieldId);
            return field!;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateField(Field Field)
        {
            // no thing
        }
    }
}