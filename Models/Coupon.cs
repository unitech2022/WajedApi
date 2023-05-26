using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    // id name discount minPrice starteDate endDate useCount userId createdAt
    public class Coupon
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? Code { get; set; }
        public double Discount { get; set; }

        public double MinPrice { get; set; }


        public int UseCount { get; set; }
        public int MaxUseCount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}