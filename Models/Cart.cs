using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{

    // Id productIod quantity  cost userId createdAt
    public class Cart
    {

        public int Id { get; set; }
        public int ProductId { get; set; }

         public int restaurantId { get; set; }

        public int Quantity { get; set; }

         public int Status { get; set; }

         public int OrderId { get; set; }
        public double Cost { get; set; }

        public string? UserId { get; set; }

         public string? Options { get; set; }
        public DateTime CreatedAt { get; set; }

        public Cart()
        {

            CreatedAt = DateTime.Now;
            OrderId=0;

        }

    }
}