using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;

namespace WajedApi.ViewModels
{
    public class ResponseMarketDetails
    {
    public Market? Market { get; set; }

    public List<Category>? Categories { get; set; }

    public List<Product>? Products { get; set; }
    }
}