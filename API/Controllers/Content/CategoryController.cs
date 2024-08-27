using Application.Common;
using Application.Dtos;
using Application.Interfaces.Content.Categories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoryController : BaseController
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        #region Get All Categories Endpoint
        // GET: CategoriesController
        [HttpGet("GET_ALLCategories")]
        public async Task<ActionResult<ApiResult<List<CreateCategoryDto>>>> GetAllCategories()
        {
            var categries = await _categoryService.GetAll();
            return HandleOperationResult(categries);

        }
        #endregion

        #region Get Category Endpoint
        // GET: CategoriesController/Details/5
        [HttpGet("GET_Category")]
        public async Task<ActionResult<ApiResult<CreateCategoryDto>>> Details(int id)
        {
            var category = await _categoryService.GetById(id);

            return HandleOperationResult(category);

        }
        #endregion

        #region Create Category Endpoint
        // POST: CategoriesController/Create
        [HttpPost("Add_NewCategory")]
        public async Task<ActionResult<ApiResult<List<CreateCategoryDto>>>> CreateCategory( CreateCategoryDto model)
        {
            model.CategoryDescription = model.CategoryDescription?.Replace("\n", "\\n");
         
            if (await _categoryService.CategoryIsExist(model.CategoryName))
                return BadRequest(" this CategoryEntity name is already registred");
            
            await  _categoryService.AddCategory(model);

            return HandleOperationResult(await _categoryService.GetAll());

        }
        #endregion

        #region Update Category
        // POST: CategoriesController/Edit/5
        [HttpPut("Edit_Category")]
        public async Task<ActionResult<ApiResult<List<CategoryBenefit>>>> UpdateCategory(int id, CreateCategoryDto item)
        {
            


          Category  cat = new()
            {
                CategoryDescription = item.CategoryDescription,
                CategoryName = item.CategoryName,
                CategoryImage = item.CategoryImage,
                CategoryVideoLink = item.CategoryVideoLink,
                CategoryId = id,
                
                
            };

          

            await _categoryService.UpdateCategory(cat, id, item.CategoryBenefits);
            return HandleOperationResult(ApiResult<List<CategoryBenefit>>.Successful(null));
        }
        #endregion

        #region Delete Category
        // POST: CategoriesController/Delete/5
        [HttpDelete("Delete_Category")]
        public async Task<ActionResult<ApiResult<Category>>> Delete(int id)
        {
           // var category = await _categoryService.GetInsuranceCoyDetailById(id);

           
           var category = await _categoryService.DeleteCategory(id);
            return HandleOperationResult(category);
            
        }
        #endregion
    }
}