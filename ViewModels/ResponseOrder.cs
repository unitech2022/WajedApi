using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;

namespace WajedApi.ViewModels
{
    public class ResponseOrder
    {

          public Order? order { get; set; }
         public  List<OrderDetails>? Products { get; set; }

       public Market? Market { get; set; }
    }
}

public class OrderDetails{

    public OrderItem? Order { get; set; }
    public Product? Product { get; set; }

    public List<ProductsOption>? Options { get; set; }
}

