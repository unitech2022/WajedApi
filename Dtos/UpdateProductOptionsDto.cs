using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateProductOptionsDto
    {
         public int ProductId { get; set; }

       
        public double Price { get; set; }

        public string? Name { get; set; }
         public string? Type { get; set; }
    }
}