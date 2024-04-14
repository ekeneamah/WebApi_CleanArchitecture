using Application.Interfaces.Content.Categories;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services
{
    public class CategoryService : ICategory
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        #region GetAll
        public async Task<List<CategoryEntity>> GetAll()
        {
            return await _context.Categories
                  .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region GetById
        public async Task<CategoryEntity> GetById(int id)
        {
            return await _context.Categories
              .FindAsync(id);
        }
        #endregion

        #region AddCategory
        public async Task<CategoryEntity> AddCategory(CategoryEntity model)
        {
            _context.Categories.Add(model);

            _context.SaveChanges();

            return model;
        }
        #endregion

        #region UpdateCategory
        public CategoryEntity UpdateCategory(CategoryEntity model)
        {
            _context.Update(model);
            _context.SaveChanges();
            return model;
        }
        #endregion

        #region DeleteCategory
        public CategoryEntity DeleteCategory(CategoryEntity model)
        {
            _context.Remove(model);
            _context.SaveChanges();
            return model;
        }
        #endregion

        #region CategoryIsExist
        public async Task<bool> CategoryIsExist(string categoryName)
        {
            return await _context.Categories.AnyAsync(p => p.Category_Name == categoryName);
        }
        #endregion
    }
}