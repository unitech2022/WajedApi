using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WajedApi.Models;
using WajedApi.Models.BaseEntity;

namespace WajedApi.Serveries.CategoriesServices
{
    public interface ICategoriesService
    {
        Task<BaseResponse> GetCategories(string UserId,int page);




        Task<Category> AddCategory(Category category);

         Task<Category> GitCategoryById(int CategoryId);


        Task<Category> DeleteCategory(int CategoryId);

        void UpdateCategory(Category category);


         bool SaveChanges();
    }
}