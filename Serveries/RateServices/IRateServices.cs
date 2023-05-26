
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models;

namespace WajedApi.Serveries.AddressesServices
{
    public interface IRateServices :BaseInterface
    {
        
        Task<dynamic> AddRate(Rate rate);
    }
}