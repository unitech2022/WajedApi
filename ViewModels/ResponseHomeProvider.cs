using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;

namespace WajedApi.ViewModels
{
    public class ResponseHomeProvider : BaseResponse
    {
        public Market? market { get; set; }

         public double Receipts { get; set; }
        public int SuccessfulOrders { get; set; }
        public int OtherOrders { get; set; }
        
        
        

    }
}