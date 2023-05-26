using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace WajedApi.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        public string? description { get; set; }



        [NotMapped]
        public bool IsCart { get; set; }
        public double? price { get; set; }
        public string? calories { get; set; }

        public int categoryId { get; set; }

        public int restaurantId { get; set; }


        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product()
        {

            CreatedAt = DateTime.Now;
            IsCart=false;

        }
    }
}