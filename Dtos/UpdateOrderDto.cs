using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateOrderDto
    {
         public int RestaurantId { get; set; }

        public int Status { get; set; }
        public string? UserId { get; set; }

        public double TotalCost { get; set; }
        public double Tax { get; set; }

        public double DeliveryCost { get; set; }

        

        public double ProductsCost { get; set; }

        public string? DriverId { get; set; }

        public string? Notes { get; set; }

    }
}