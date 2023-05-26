using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateOfferDto
    {
         public string? Image { get; set; }

        public int Status { get; set; }
        public string? ProductId { get; set; }

        public double discount { get; set; }

    }
}