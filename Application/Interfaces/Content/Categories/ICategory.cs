using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Content.Categories
{
    public interface ICategory
    {
        Task<List<CategoryEntity>> GetAll();


        Task<CategoryEntity> GetById(int id);


        Task<CategoryEntity> AddCategory(CategoryEntity model);



        CategoryEntity UpdateCategory(CategoryEntity model);



        CategoryEntity DeleteCategory(CategoryEntity model);



        Task<bool> CategoryIsExist(string categoryName);

    }
}
