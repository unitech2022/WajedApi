using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateCategoryDto
    {
         public string? Name { get; set; }
          public string? ImageUrl { get; set; }

        public int Status { get; set; }
    }
}