using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Helpers;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using WajedApi.ViewModels;
using X.PagedList;

namespace WajedApi.Serveries.MarketsService
{
    public class MarketService : IMarketsService
    {

        private readonly AppDBcontext _context;
        private IMapper _mapper;

        public MarketService(AppDBcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<dynamic> AddAsync(dynamic type)
        {
            await _context.Markets!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;

        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {
            Market? market = await _context.Markets!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (market != null)
            {
                _context.Markets!.Remove(market);

                await _context.SaveChangesAsync();
            }

            return market!;
        }

        public Task<dynamic> GetItems(string UserId, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMarketDetails> GetMarketDetails(int marketId, string userId)
        {


            // List<Product> allproduct=new  List<Product>();
            Market? market = await _context.Markets!.FirstOrDefaultAsync(x => x.Id == marketId);
            List<Product> products = await _context.Products!.Where(x => x.restaurantId == marketId).ToListAsync();
            List<Category> categories = await _context.Categories!.ToListAsync();
            foreach (Product item in products)
            {
                Cart? cart = await _context.Carts!.FirstOrDefaultAsync(t => t.ProductId == item.Id && t.UserId == userId);
                if (cart != null)
                {
                    item.IsCart = true;
                }


            }
            ResponseMarketDetails responseMarketDetails = new ResponseMarketDetails
            {
                Market = market,
                Products = products,
                Categories = categories
            };

            return responseMarketDetails;

        }



        public async Task<List<Market>> SearchMarket(string textSearch, int AddressId)
        {

            List<Market> markets = new List<Market>();
            List<Market> allMarkets = await _context.Markets!.Where(p => p.Title!.Contains(textSearch)).ToListAsync();
            Address? userAddress = await _context.Addresses!.FirstOrDefaultAsync(x => x.Id == AddressId);
            foreach (var market in allMarkets)
            {

                double distance = Functions.GetDistance(market.Lat, userAddress!.Lat, market.Lng, userAddress.Lng);
                if (distance < 30)
                {
                    market.Distance = distance;
                    markets.Add(market);
                }

            }

            return markets;

        }

        public async Task<dynamic> GitById(int typeId)
        {
            Market? market = await _context.Markets!.FirstOrDefaultAsync(x => x.Id == typeId);
            return market!;
        }



        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(dynamic category)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> GetMarketsByFieldId(int fieldId, int AddressId, int page)
        {
            List<Market> markets = new List<Market>();
            List<Market> allMarkets = await _context.Markets!.Where(t => t.FieldId == fieldId).ToListAsync();
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.Id == AddressId);
            foreach (Market item in allMarkets)
            {
                double distance = Functions.GetDistance(address!.Lat, address.Lng, item.Lat, item.Lng);
                if (distance <= 30)
                {

                    markets.Add(item);
                }

            }

            var pageResults = 10f;
            var pageCount = Math.Ceiling(markets.Count() / pageResults);

            var items = await markets
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
    }
}