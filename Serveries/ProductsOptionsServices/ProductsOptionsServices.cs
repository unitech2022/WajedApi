using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Core;
using WajedApi.Data;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using X.PagedList;

namespace WajedApi.Serveries.ProductsOptionsServices
{
    public class ProductsOptionsServices : IProductsOptionsServices
    {

       
        private readonly IMapper _mapper;

       
        private readonly AppDBcontext _context;

        public ProductsOptionsServices(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<dynamic> AddAsync(dynamic type)
        {
             await _context.ProductsOptions!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;
        
            
        
        }


   public async Task<dynamic> GetItems(string UserId, int page)
        {
           List<ProductsOption> productsOptions = await _context.ProductsOptions!.ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(productsOptions.Count() / pageResults);

            var items = await productsOptions
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


        public async Task<dynamic> DeleteAsync(int typeId)
        {
             ProductsOption? productOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (productOption != null)
            {
                _context.ProductsOptions!.Remove(productOption);

                await _context.SaveChangesAsync();
            }

            return productOption!;
        }

     

        public async Task<dynamic> GitById(int typeId)
        {
            ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == typeId);
            return productsOption!;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(dynamic category)
        {//  throw new NotImplementedException();
        }

        public async Task<BaseResponse> GitProductOptionsByProductId(string UserId, int productId, int page)
        {

             List<ProductsOption> categories = await _context.ProductsOptions!.Where(x=> x.ProductId==productId).ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(categories.Count() / pageResults);

            var items = await categories
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