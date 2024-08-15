using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Content.Categories
{
    public interface ICategory
    {
        public Task<List<CreateCategoryDto>> GetAll();


        Task<CreateCategoryDto> GetById(int id);


        Task<CreateCategoryDto> AddCategory(CreateCategoryDto model);



        Task<int> UpdateCategory(Category model, int category_id, List<CategoryBenefit> categoryBenefit);



        Task<Category> DeleteCategory(int category_id);



        Task<bool> CategoryIsExist(string categoryName);

    }
}
