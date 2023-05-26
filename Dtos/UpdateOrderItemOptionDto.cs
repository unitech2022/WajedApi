using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateOrderItemOptionDto
    {
          public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int OptionId { get; set; }
        public double Price { get; set; }

 
    }
}