using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    public class Market
    {
        public int Id { get; set; }


         public int FieldId { get; set; }
        public string? Title { get; set; }

        public string? Desc { get; set; }


        public string? UserId { get; set; }

         public string? ImageUrl { get; set; }

        public string? Options { get; set; }

        public string? AddressName { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public double Rate { get; set; }

        public int Status { get; set; }

        public double discount { get; set; }

          public double Distance { get; set; }

        public DateTime CreatedAt { get; set; }
        public Market()
        {

            CreatedAt = DateTime.Now;
            Distance=0.0;

        }

    }
}