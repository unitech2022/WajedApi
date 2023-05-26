using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models;

namespace WajedApi.Serveries.AlertsServices
{
    public interface IAlertsServices :BaseInterface
    {
         Task<List<Alert>> GetAlertsByUserId(string userId,int page);
    }
}