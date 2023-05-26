using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateProductDto
    {
        
       
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        public string? description { get; set; }




        public double? price { get; set; }
        public string? calories { get; set; }

        public int categoryId { get; set; }

        public int restaurantId { get; set; }


        public int Status { get; set; }
    }
}