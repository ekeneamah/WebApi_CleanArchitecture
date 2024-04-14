using Application.Interfaces.Content.Categories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanaArchitecture1.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        #region Get All Categories Endpoint
        // GET: CategoriesController
        [HttpGet("GET_ALLCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categries = await _categoryService.GetAll();
            if (categries is null)
                return NotFound("No data here!");
            return Ok(categries);
        }
        #endregion

        #region Get Category Endpoint
        // GET: CategoriesController/Details/5
        [HttpGet("GET_Category")]
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound($"No CategoryEntity has found with this {id} ");

            return Ok(category);
        }
        #endregion

        #region Create Category Endpoint
        // POST: CategoriesController/Create
        [HttpPost("Add_NewCategory")]
        public async Task<IActionResult> CreateCategory([FromQuery] CategoryEntity model)
        {
            model.Category_Description = model.Category_Description.Replace("\n", "\\n");
            if (ModelState.IsValid is false)
            {
                return BadRequest("Invalid Inputs");
            }

            if (await _categoryService.CategoryIsExist(model.Category_Name))
                return BadRequest(" this CategoryEntity name is already registred");

            var categy = new CategoryEntity
            {
                Category_Name = model.Category_Name,
                Category_Description = model.Category_Description,
                Category_VideoLink = model.Category_VideoLink,
                Category_Benefits = model.Category_Benefits,
                Category_Image = model.Category_Image
            };

            _categoryService.AddCategory(categy);

            return Ok(await _categoryService.GetAll());
        }
        #endregion

        #region Update Category
        // POST: CategoriesController/Edit/5
        [HttpPut("Edit_Category")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryEntity model)
        {
            var category = await _categoryService.GetById(id);


            if (category == null)
                return NotFound($"No CategoryEntity was found with ID {id}");

           

            category.Category_Name = model.Category_Name;
            category.Category_Description = model.Category_Description.Replace("\n", "\\n");
            category.Category_VideoLink = model.Category_VideoLink;
            category.Category_Benefits = model.Category_Benefits;
            category.Category_Image = model.Category_Image;
            _categoryService.UpdateCategory(category);

            return Ok(category);
        }
        #endregion

        #region Delete Category
        // POST: CategoriesController/Delete/5
        [HttpDelete("Delete_Category")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound($"No Ctegory was found with ID {id}");

            _categoryService.DeleteCategory(category);

            return Ok($"CategoryEntity : {category.Category_Name} with Id : ({category.Categoty_Id}) is deleted");
        }
        #endregion
    }
}