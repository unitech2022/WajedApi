using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using WajedApi.ViewModels;
using WajedApi.Dtos;

namespace WajedApi.Serveries.CartsService
{
    public interface ICartsService
    {
          Task<ResponseCart> GetCarts(string UserId,string code,int AddressId);


        // Task<BaseResponse> GitCartsByCategoryId(string UserId,int categoryId,int page );

         Task<Cart> AddCart(Cart Cart);

         Task<Cart> GitCartById(int CartId);


         Task<Cart> DeleteCart(int CartId);

        Task<Cart> UpdateCart(Cart cart);


         bool SaveChanges(); 
    }
}