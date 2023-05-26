using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WajedApi.Data;
using WajedApi.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using WajedApi.Models.BaseEntity;

using X.PagedList;

namespace WajedApi.Serveries.CategoriesServices
{
    public class CategoriesService : ICategoriesService

    {


        private readonly IMapper _mapper;

        private readonly IConfiguration _config;
        private readonly AppDBcontext _context;

        public CategoriesService(IMapper mapper, IConfiguration config, AppDBcontext context)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {

            await _context.Categories!.AddAsync(category);

            await _context.SaveChangesAsync();

            return category;

        }

      
        public async Task<BaseResponse> GetCategories(string UserId, int page)
        {
            List<Category> categories = await _context.Categories!.ToListAsync();
           


            var pageResults = 10f;
            var pageCount = Math.Ceiling(categories.Count() / pageResults);

            var items = await categories
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();



            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;

        }

        public async Task<Category> GitCategoryById(int CategoryId)
        {

            Category? category = await _context.Categories!.FirstOrDefaultAsync(x => x.Id == CategoryId);
            return category!;
        }

        public void UpdateCategory(Category category)
        {


            // nothing

        }

  public async Task<Category> DeleteCategory(int CategoryId)
        {
            Category? category = await _context.Categories!.FirstOrDefaultAsync(x => x.Id == CategoryId);

            if (category != null)
            {
                _context.Categories!.Remove(category);

                await _context.SaveChangesAsync();
            }

            return category!;
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}