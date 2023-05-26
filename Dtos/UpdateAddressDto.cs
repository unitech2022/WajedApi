using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateAddressDto
    {
           public string? UserId { get; set; }

        public string? Description { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}