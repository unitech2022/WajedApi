using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    public class ResponseHome
    {
      public  List<Market>? Markets { get; set; }
      public  List<Field>? Fields { get; set; }
      public  List<Category>? Categories{ get; set; }
      public  List<Offer>? Offers { get; set; }

  public bool HasDefaultAddress { get; set; }
      public  int addressId { get; set; }

      
    }
}



