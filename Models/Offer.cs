using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{


    public class Offer
    {
        public int Id { get; set; }
        public string? Image { get; set; }

        public int Status { get; set; }
        public string? ProductId { get; set; }

        public double discount { get; set; }

        public DateTime CreatedAt { get; set; }
        public Offer()
        {

            CreatedAt = DateTime.Now;

        }
    }
}