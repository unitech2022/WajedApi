using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WajedApi.Models
{

    // id userId restaurantId totlalCost Tax 
    // deliveryCost driverId productsCost 
    // status notes CreatedAt
    public class Order
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        public int Status { get; set; }
        public string? UserId { get; set; }

        public double TotalCost { get; set; }
        public double Tax { get; set; }

        public double DeliveryCost { get; set; }

        public double ProductsCost { get; set; }

        public string? DriverId { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order()
        {

            CreatedAt = DateTime.Now;
            Status=0;

        }

        public static implicit operator Order(EntityEntry<Order> v)
        {
            return new Order();
        }
    }
}