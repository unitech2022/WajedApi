using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    public class OrderItem
    {
         public int Id { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

         public string? Options { get; set; }
        public int OrderId { get; set; }
        public double Cost { get; set; }

        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public OrderItem()
        {

            CreatedAt = DateTime.Now;

        }
    }
}