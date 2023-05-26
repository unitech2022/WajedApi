using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;

namespace WajedApi.Serveries.ProductsService
{
    public interface IProductsService
    {
        
         Task<BaseResponse> GetProducts(string UserId,int page);


        Task<BaseResponse> GitProductsByCategoryId(string UserId,int categoryId,int page );

         Task<Product> AddProduct(Product Product);

         Task<Product> GitProductById(int ProductId);


 Task<Product> GitProductDetails(int ProductId,string UserId);
         Task<Product> DeleteProduct(int ProductId);

        void UpdateProduct(Product Product);


         bool SaveChanges();
    }
}