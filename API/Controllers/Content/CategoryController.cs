﻿using Application.Common;
using Application.Dtos;
using Application.Interfaces.Content.Categories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Content
{
    [Route("api/categories")]
    [ApiController]
   
    public class CategoryController : BaseController
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        #region Get All Categories Endpoint
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<CreateCategoryDto>>>> GetAllCategories()
        {
            var categries = await _categoryService.GetAll();
            return HandleOperationResult(categries);

        }
        #endregion

        #region Get Category Endpoint
        // GET: CategoriesController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<CreateCategoryDto>>> Details([FromRoute] int id)
        {
            var category = await _categoryService.GetById(id);

            return HandleOperationResult(category);

        }
        #endregion

        #region Create Category Endpoint
        // POST: CategoriesController/Create
        [HttpPost]
        public async Task<ActionResult<ApiResult<List<CreateCategoryDto>>>> CreateCategory([FromBody] CreateCategoryDto model)
        {
            model.CategoryDescription = model.CategoryDescription?.Replace("\n", "\\n");
            var response = await  _categoryService.AddCategory(model);
            if (!response.Success)
                return HandleOperationResult(ApiResult<List<CreateCategoryDto>>.Failed(response.Message));
                
            return HandleOperationResult(await _categoryService.GetAll());

        }
        #endregion

        #region Update Category
        // POST: CategoriesController/Edit/5
        [HttpPut]
        public async Task<ActionResult<ApiResult<List<CategoryBenefit>>>> UpdateCategory(int id,[FromBody] CreateCategoryDto item)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Category>>> Delete([FromRoute] int id)
        {
           // var category = await _categoryService.GetInsuranceCoyDetailById(id);

           
           var category = await _categoryService.DeleteCategory(id);
            return HandleOperationResult(category);
            
        }
        #endregion
    }
}