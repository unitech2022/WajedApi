using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    public class ProductsOption
    {
         public int Id { get; set; }
        public int ProductId { get; set; }

       
        public double Price { get; set; }

        public string? Name { get; set; }
         public string? Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductsOption()
        {

            CreatedAt = DateTime.Now;

        }
    }
}