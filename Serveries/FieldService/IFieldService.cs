using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;

namespace WajedApi.Serveries.FieldService
{
    public interface IFieldService
    {
         Task<BaseResponse> GetFields(string UserId,int page);




        Task<Field> AddField(Field Field);

         Task<Field> GitFieldById(int FieldId);


        Task<Field> DeleteField(int FieldId);

        void UpdateField(Field Field);


         bool SaveChanges();
    }
}