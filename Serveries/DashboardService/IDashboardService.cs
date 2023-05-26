using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Serveries.DashboardService
{
    public interface IDashboardService
    {
        
         Task<Object> GetHome();

Task<Object> GetMarkets(int page);

       


         bool SaveChanges();
    }
}