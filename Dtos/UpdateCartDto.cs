using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateCartDto
    {
        public int ProductId { get; set; }
 public int restaurantId { get; set; }
        public int Quantity { get; set; }

        public int Status { get; set; }
        public double Cost { get; set; }

        public int OrderId { get; set; }

        public string? Options { get; set; }

        public string? UserId { get; set; }
    }
}