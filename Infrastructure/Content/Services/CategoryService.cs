using Application.Dtos;
using Application.Interfaces.Content.Categories;
using Domain.Entities;
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
        public async Task<List<CategoryDTO>> GetAll()
        {
            List<CategoryDTO> result = new List<CategoryDTO>();
            List<Category> c =  await _context.Categories
                  .AsNoTracking()
                .ToListAsync();
            foreach (Category item in c)
            {
                CategoryDTO cd = new()
                {
                    Category_Description = item.Category_Description,
                    Category_Name = item.Category_Name,
                    Category_Benefits = await _context.CategoryBenefits.Where(c=>c.Benefit_Category_id==item.Category_Id).ToListAsync(),
                    Category_Image = item.Category_Image,
                    Category_VideoLink = item.Category_VideoLink,
                    category_id = item.Category_Id
                };
                result.Add(cd);
            }
            return result;
        }
        #endregion

        #region GetById
        public async Task<CategoryDTO> GetById(int id)
        {
            Category item =  await _context.Categories
              .FindAsync(id);
            
                CategoryDTO cd = new()
                {
                    Category_Description = item.Category_Description,
                    Category_Name = item.Category_Name,
                    Category_Benefits = await _context.CategoryBenefits.Where(c => c.Benefit_Category_id == item.Category_Id).ToListAsync(),
                    Category_Image = item.Category_Image,
                    Category_VideoLink = item.Category_VideoLink,
                    category_id = item.Category_Id
                };

            return cd;
        }
        #endregion

        #region AddCategory
        public async Task<CategoryDTO> AddCategory(CategoryDTO item)
        {

            Category model = new()
            {
                Category_Description = item.Category_Description,
                Category_Name = item.Category_Name,               
                Category_Image = item.Category_Image,
                Category_VideoLink = item.Category_VideoLink,
            };
            _context.Categories.Add(model);

            await _context.SaveChangesAsync();
            foreach (CategoryBenefit c in item.Category_Benefits) {
                c.Benefit_Category_id = model.Category_Id;
                _context.CategoryBenefits.Add(c);
               await _context.SaveChangesAsync();
            }

            return item;
        }
        #endregion

        #region UpdateCategory
        public async Task<int> UpdateCategory(Category item,int category_id,List<CategoryBenefit> categoryBenefit)
        {
            int result = 0;
          //  CategoryDTO cd = new CategoryDTO();
            try
            {
                Category category = await _context.Categories.FindAsync(category_id);
           

            category = item;
            _context.Entry(category).State= EntityState.Modified;
            
           result =  await _context.SaveChangesAsync();
            CategoryBenefit cb = await _context.CategoryBenefits.Where(b => b.Benefit_Category_id == category_id).FirstOrDefaultAsync();
            if (cb != null)
            {
                _context.Remove(cb);
                await _context.SaveChangesAsync();
            }
            foreach (CategoryBenefit c in categoryBenefit)
            {
              
                c.Benefit_Category_id = category_id;
                _context.CategoryBenefits.Add(c);
               await _context.SaveChangesAsync();
            }
             CategoryDTO cd = new()
            {
                 category_id = category_id,
                 Category_Benefits = categoryBenefit,
                 Category_Description= item.Category_Description,
                 Category_Name= item.Category_Name,
                 Category_VideoLink= item.Category_VideoLink,
            };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion

        #region DeleteCategory
        public async Task<Category> DeleteCategory(int category_id)
        {
            CategoryBenefit cb = await _context.CategoryBenefits.Where(b=>b.Benefit_Category_id==category_id).FirstOrDefaultAsync();
            Category model = await _context.Categories.FindAsync(category_id);
            if (cb != null)
            {
                _context.Remove(cb);
                await _context.SaveChangesAsync();
            }
            _context.Remove(model);
            await _context.SaveChangesAsync();
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