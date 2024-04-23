using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces.Content.Categories
{
    public interface ICategory
    {
        public Task<List<CategoryDTO>> GetAll();


        Task<CategoryDTO> GetById(int id);


        Task<CategoryDTO> AddCategory(CategoryDTO model);



        Task<int> UpdateCategory(Category model, int category_id, List<CategoryBenefit> categoryBenefit);



        Task<Category> DeleteCategory(int category_id);



        Task<bool> CategoryIsExist(string categoryName);

    }
}
