using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Core;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;
using WajedApi.ViewModels;

namespace WajedApi.Serveries.MarketsService
{
    public interface IMarketsService : BaseInterface


    {
        Task<ResponseMarketDetails> GetMarketDetails(int marketId,string userId);

         Task<List<Market>> SearchMarket(string  textSearch,int AddressId);

          Task<BaseResponse> GetMarketsByFieldId(int fieldId,int AddressId,int page);
    }
}