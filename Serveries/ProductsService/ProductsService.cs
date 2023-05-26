using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using WajedApi.ViewModels;
using X.PagedList;

namespace WajedApi.Serveries.ProductsService
{
    public class ProductsService : IProductsService
    {


        private readonly IMapper _mapper;
        private readonly AppDBcontext _context;

        public ProductsService(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            Market? market=await _context.Markets!.FirstOrDefaultAsync(t => t.Id==product.restaurantId);
            if(market ==null){

               
            }
            await _context.Products!.AddAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteProduct(int ProductId)
        {
            Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == ProductId);

            if (product != null)
            {
                _context.Products!.Remove(product);
                Cart? cart =await _context.Carts!.FirstOrDefaultAsync(t => t.ProductId ==ProductId);
                if(cart !=null){
                    _context.Carts!.Remove(cart);
                }
                await _context.SaveChangesAsync();
            }

            return product!;
        }

        public async Task<BaseResponse> GetProducts(string UserId, int page)
        {
            List<ResponseProduct> productsResponse = new List<ResponseProduct>();
            List<Product> products = await _context.Products!.ToListAsync();

            foreach (Product item in products)
            {
                var cart = await _context.Carts!.FirstOrDefaultAsync(x => x.ProductId==item.Id&&x.UserId==UserId);
                 if(cart!=null){
                    item.Status =cart!.Quantity; 
                 }
                

                List<ProductsOption> options = await _context.ProductsOptions!.Where(x => x.ProductId == item.Id).ToListAsync();
                ResponseProduct responseProduct = new ResponseProduct
                {
                    Product = item,
                    productsOptions = options
                };
                productsResponse.Add(responseProduct);


            }




            var pageResults = 10f;
            var pageCount = Math.Ceiling(productsResponse.Count() / pageResults);

            var items = await productsResponse
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

        public async Task<Product> GitProductById(int ProductId)
        {
            Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == ProductId);
             
            return product!;
        }

        public async Task<Product> GitProductDetails(int ProductId, string UserId)
        {


              Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == ProductId);
              var cart = await _context.Carts!.FirstOrDefaultAsync(x => x.ProductId==ProductId&&x.UserId==UserId);
                 if(cart!=null){
                    product!.Status =cart!.Quantity; 
                 }
            return product!;
           
        }

        public async Task<BaseResponse> GitProductsByCategoryId(string UserId, int categoryId, int page)
        {
            List<Product> products = await _context.Products!.Where(x => x.categoryId == categoryId).ToListAsync();
              foreach (var item in products)
              {
                 var cart = await _context.Carts!.FirstOrDefaultAsync(x => x.ProductId==item!.Id&&x.UserId==UserId);
                 if(cart!=null){
                    item!.Status =cart!.Quantity; 
                 }
              }
                    
            
            var pageResults = 10f;
            var pageCount = Math.Ceiling(products.Count() / pageResults);

            var items = await products
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

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateProduct(Product Product)
        {
            /// no thing 
        }
    }
}