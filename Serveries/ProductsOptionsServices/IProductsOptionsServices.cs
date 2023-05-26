using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models.BaseEntity;

namespace WajedApi.Serveries.ProductsOptionsServices
{
    public interface IProductsOptionsServices : BaseInterface
    {
        
        



        Task<BaseResponse> GitProductOptionsByProductId(string UserId,int productId,int page );

      
        
    }
}