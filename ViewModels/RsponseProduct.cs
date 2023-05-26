using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;

namespace WajedApi.ViewModels
{
    public class ResponseProduct
    {
        public Product? Product { get; set; }

        public List<ProductsOption>? productsOptions { get; set; }
    }
}