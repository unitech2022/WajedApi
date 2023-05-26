using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models;
using WajedApi.ViewModels;

namespace WajedApi.Serveries.HomeService
{
    public interface IHomeService : BaseInterface
    {
          Task<ResponseHome> GetHomeData(string UserId,int AddressId);

           Task<ResponseHomeProvider> GetHomeDataProvider(string UserId,int page);
    }
}