using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models.BaseEntity
{
    public class BaseResponse
    {

     public IEnumerable<Object>? Items { get; set; }
     
     public int CurrentPage { get; set; }

     public int TotalPages { get; set; }
       
    }
}