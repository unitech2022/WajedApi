using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Data;
using AutoMapper;
using WajedApi.Models;
using Microsoft.EntityFrameworkCore;
using WajedApi.Helpers;
using WajedApi.ViewModels;
using X.PagedList;

namespace WajedApi.Serveries.HomeService
{

    public class HomeService : IHomeService
    {

        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;

        public HomeService(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ResponseHome> GetHomeData(string UserId, int AddressId)
        {
            bool hasDefaultAddress=false;
            int addressId = 0;

            List<Market>? markets = new List<Market>();
            List<Field>? fields = new List<Field>();
            List<Category>? categories = new List<Category>();
            List<Offer>? offers = new List<Offer>();
            Address? userAddress = await _context.Addresses!.FirstOrDefaultAsync(x => x.Id == AddressId&& x.DefaultAddress==true);
            List<Market>? AllMarkets = await _context.Markets!.ToListAsync();

            //   get Distance
            if(userAddress!=null){
                hasDefaultAddress=true;
                addressId =userAddress.Id;
               foreach (var market in AllMarkets)
            {
                double distance = Functions.GetDistance(market.Lat, userAddress!.Lng, market.Lng, userAddress.Lat);
                if (distance < 30)
                {
                    market.Distance=distance;
                    markets.Add(market);
                }
            }

            }else {
              markets =AllMarkets;
            }
           
            fields = await _context.Fields!.ToListAsync();
            categories = await _context.Categories!.ToListAsync();
            offers = await _context.Offers!.ToListAsync();
            ResponseHome responseHome = new ResponseHome
            {
                HasDefaultAddress=hasDefaultAddress,
                addressId=addressId,
                Markets = AllMarkets,
                Fields = fields,
                Categories = categories,
                Offers = offers

            };


            return responseHome;



        }


     //provider
     public async Task<ResponseHomeProvider> GetHomeDataProvider(string UserId,int page)
        {

            List<ResponseOrder> responseOrders=new List<ResponseOrder>();
            Market? market = await _context.Markets!.FirstOrDefaultAsync(t => t.UserId==UserId);


           List<Order> orders=await _context.Orders!.Where(t => t.RestaurantId == market!.Id).ToListAsync();
           foreach (var item in orders)
           {
           responseOrders.Add(await Functions.getOrderDetails(item.Id,_context));
            
           }


           int successful= responseOrders.Where(t => t.order!.Status==5).Count();
           int other =responseOrders.Where(t => t.order!.Status!=5).Count();
           double receipts =responseOrders.Sum(i => i.order!.TotalCost);
         

             var pageResults = 10f;
            var pageCount = Math.Ceiling(responseOrders.Count() / pageResults);

            var items = await responseOrders
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
           ResponseHomeProvider responseHomeProvider =new ResponseHomeProvider
           {
            market=market,
            Receipts=receipts,
            SuccessfulOrders=successful,
            OtherOrders=other,
               Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
              

           };

            return responseHomeProvider;



        }





        public Task<dynamic> AddAsync(dynamic type)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> DeleteAsync(int typeId)
        {
            throw new NotImplementedException();
        }


        public Task<dynamic> GetItems(string UserId, int page)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GitById(int typeId)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(dynamic category)
        {
            throw new NotImplementedException();
        }
    }
}