using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateOrderItemDto
    {
           public int ProductId { get; set; }

        public int Quantity { get; set; }
        public double Cost { get; set; }

        public string? UserId { get; set; }
    }
}