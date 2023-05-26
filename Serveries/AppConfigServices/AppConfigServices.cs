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

namespace WajedApi.Serveries.AppConfigServices
{
    public class AppConfigServices : IAppConfigServices
    {


   private readonly IMapper _mapper;

       
        private readonly AppDBcontext _context;

        public AppConfigServices(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async  Task<dynamic> AddAsync(dynamic type)
        {
            await _context.AppConfigs!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;
        }

       

        public async  Task<dynamic> GetItems(string UserId, int page)
        {
            List<AppConfig> alerts = await _context.AppConfigs!.ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(alerts.Count() / pageResults);

            var items = await alerts
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

        public  async Task<dynamic> GitById(int typeId)
        {
            AppConfig? appConfig = await _context.AppConfigs!.FirstOrDefaultAsync(x => x.Id == typeId);
            return appConfig!;
        }




        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(dynamic category)
        {
           // 
        }

         public async  Task<dynamic> DeleteAsync(int typeId)
        {
               AppConfig? appConfig = await GitById(typeId);

            if (appConfig != null)
            {
                _context.AppConfigs!.Remove(appConfig);

                await _context.SaveChangesAsync();
            }

            return appConfig!;
        }
    }
}