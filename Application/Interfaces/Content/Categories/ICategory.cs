using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Content.Categories
{
    public interface ICategory
    {
        public Task<ApiResult<List<CreateCategoryDto>>> GetAll();


        Task<ApiResult<CreateCategoryDto>> GetById(int id);


        Task<ApiResult<CreateCategoryDto>> AddCategory(CreateCategoryDto model);



        Task<int> UpdateCategory(Category model, int category_id, List<CategoryBenefit> categoryBenefit);



        Task<ApiResult<Category>> DeleteCategory(int category_id);



        Task<bool> CategoryIsExist(string categoryName);

    }
}
