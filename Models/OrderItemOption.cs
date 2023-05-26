using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    public class OrderItemOption
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int OptionId { get; set; }
        public double Price { get; set; }



        public DateTime CreatedAt { get; set; }

        public OrderItemOption()
        {

            CreatedAt = DateTime.Now;

        }
    }
}