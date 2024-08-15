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
        public async Task<List<CreateCategoryDto>> GetAll()
        {
            List<CreateCategoryDto> result = new List<CreateCategoryDto>();
            List<Category> c =  await _context.Categories
                  .AsNoTracking()
                .ToListAsync();
            foreach (Category item in c)
            {
                CreateCategoryDto cd = new()
                {
                    CategoryDescription = item.CategoryDescription,
                    CategoryName = item.CategoryName,
                    CategoryBenefits = await _context.CategoryBenefits.Where(c=>c.BenefitCategoryId==item.CategoryId).ToListAsync(),
                    CategoryImage = item.CategoryImage,
                    CategoryVideoLink = item.CategoryVideoLink,
                    CategoryId = item.CategoryId
                };
                result.Add(cd);
            }
            return result;
        }
        #endregion

        #region GetById
        public async Task<CreateCategoryDto> GetById(int id)
        {
            Category item =  await _context.Categories
              .FindAsync(id);
            
                CreateCategoryDto cd = new()
                {
                    CategoryDescription = item.CategoryDescription,
                    CategoryName = item.CategoryName,
                    CategoryBenefits = await _context.CategoryBenefits.Where(c => c.BenefitCategoryId == item.CategoryId).ToListAsync(),
                    CategoryImage = item.CategoryImage,
                    CategoryVideoLink = item.CategoryVideoLink,
                    CategoryId = item.CategoryId
                };

            return cd;
        }
        #endregion

        #region AddCategory
        public async Task<CreateCategoryDto> AddCategory(CreateCategoryDto item)
        {

            Category model = new()
            {
                CategoryDescription = item.CategoryDescription,
                CategoryName = item.CategoryName,               
                CategoryImage = item.CategoryImage,
                CategoryVideoLink = item.CategoryVideoLink,
            };
            _context.Categories.Add(model);

            await _context.SaveChangesAsync();
            foreach (CategoryBenefit c in item.CategoryBenefits) {
                c.BenefitCategoryId = model.CategoryId;
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
            CategoryBenefit cb = await _context.CategoryBenefits.Where(b => b.BenefitCategoryId == category_id).FirstOrDefaultAsync();
            if (cb != null)
            {
                _context.Remove(cb);
                await _context.SaveChangesAsync();
            }
            foreach (CategoryBenefit c in categoryBenefit)
            {
              
                c.BenefitCategoryId = category_id;
                _context.CategoryBenefits.Add(c);
               await _context.SaveChangesAsync();
            }
             CreateCategoryDto cd = new()
            {
                 CategoryId = category_id,
                 CategoryBenefits = categoryBenefit,
                 CategoryDescription= item.CategoryDescription,
                 CategoryName= item.CategoryName,
                 CategoryVideoLink= item.CategoryVideoLink,
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
            CategoryBenefit cb = await _context.CategoryBenefits.Where(b=>b.BenefitCategoryId==category_id).FirstOrDefaultAsync();
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
            return await _context.Categories.AnyAsync(p => p.CategoryName == categoryName);
        }
        #endregion
    }
}