using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{

    // id userId name description lat lng createAt
    public class Address
    {

        public int Id { get; set; }
        public string? UserId { get; set; }

        public string? Description { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }

        public double Lng { get; set; }

         public bool DefaultAddress { get; set; }

        public DateTime CreatedAt { get; set; }
        public Address()
        {

            CreatedAt = DateTime.Now;
            DefaultAddress=false;

        }
    }
}