using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Dtos
{
    public class UpdateAlertDto
    {
         public string? UserId { get; set; }

        public string? Description { get; set; }
        public string? Page { get; set; }
        public int PageId { get; set; }
    }
}