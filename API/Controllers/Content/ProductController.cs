using API.helper;
using Application.Dtos;
using Application.Interfaces.Content.Products;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Drawing.Printing;
using Application.Common;
using Domain.Entities;

namespace API.Controllers.Content
{
    [Route("api/products")]
    [ApiController]
  
    public class ProductController : BaseController
    {
        private readonly IProduct _productServcie;

        public ProductController(IProduct productServcie)
        {
            _productServcie = productServcie;

        }

        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ProductDtoDetails>>>> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetAll(pageNumber, pageSize);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return HandleOperationResult(products);
        }
        #endregion

        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet("by-group")]
        public async Task<ActionResult<ApiResult<List<ProductDtoDetails>>>> GetProductsByGroup(string groupName,int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetAllProductsByGroup(pageNumber, pageSize,groupName);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return HandleOperationResult(products);
        }
        #endregion

        #region GetAllProducts by category id Endpoint
        // GET: api/Products
        [HttpGet("by-category-id/{categoryId}")]
        public async Task<ActionResult<ApiResult<List<ProductDtoDetails>>>> GetProductsByCategoryId(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetAllProductsByCategory( pageNumber, pageSize, categoryId);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return HandleOperationResult(products);
        }
        #endregion
        #region GetAllProducts by insurance id Endpoint
        // GET: api/Products
        [HttpGet("by-insurance-coy/{coyId}")]
        public async Task<ActionResult<ApiResult<List<Product>>>> GetProductsByInsuranceCoyId(int coyId, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetProductsByInsuranceCoyId(pageNumber, pageSize, coyId);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return HandleOperationResult(products);
        }
        #endregion
        #region Get Recommended Products Endpoint
        // GET: api/Products
        [HttpGet("recommended")]
        public async Task<ActionResult<ApiResult<List<Product>>>> GetRecommendedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetRecommendedProducts(pageNumber, pageSize);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return HandleOperationResult(products);
        }
        #endregion
        #region get product details

        // GET: api/Products/5
        [HttpGet("by-code/{code}")]
        public async Task<ActionResult<ApiResult<ProductDtoDetails>>> GetProduct(string code)
        {
            
            var product = await _productServcie.GetByCode(code);
            return HandleOperationResult(product);
        }
        #endregion

        #region get product details

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<ProductDtoDetails>>> GetProductDetailById(int id)
        {

            var product = await _productServcie.GetDetailsById(id);

            return HandleOperationResult(product);
        }
        #endregion


        #region Create Product Endpoint
        // POST: api/Products
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApiResult<Product>>> PostProduct([FromBody] CreateProductDto model)
        {

            var isExist = await _productServcie.ProductIsExist(model.ProductCode);

            if (isExist)
            {
                return HandleOperationResult(ApiResult<Product>.Failed("Code already exist"));

            }
            else
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    CoyId = model.CoyId,
                    CategoryId = model.CategoryId,
                    ProductPrice = model.ProductPrice,
                    ProductQuantity = model.ProductQuantity,
                    ProductCode = model.ProductCode,
                    ProductDescription = model.ProductDescription,
                    ProductGroup = model.ProductGroup,
                    RequireInspection = model.RequireInspection,
                    CoyProductId = model.CoyProductId
                    
                };
                return HandleOperationResult(ApiResult<Product>.Successful(await _productServcie.Add(product)));

            }
           
            //return Ok();
        }
        #endregion

        #region Update Product Endpoint
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Product>>> UpdateProduct([FromQuery] int id, [FromBody] CreateProductDto model)
        {
            var product = await _productServcie.GetById(id);

            if (product == null)
            {
                return NotFound($"brand:{model.ProductName}  has not found");
            }

            product.ProductName = model.ProductName;
            product.CoyId = model.CoyId;
            product.ProductPrice = model.ProductPrice;
            product.ProductQuantity = model.ProductQuantity;
            product.ProductDescription = model.ProductDescription;
            product.CategoryId = model.CategoryId;
            product.ProductCode = model.ProductCode;
            product.RequireInspection = model.RequireInspection;
            product.CoyProductId = model.CoyProductId;


            _productServcie.Update(product);

            return HandleOperationResult(ApiResult<Product>.Successful(product));
        }

        #endregion

        #region Delete Product Endpoint
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productServcie.GetById(id);
            if (product is null)
            {
                return NotFound($"brand:{product.ProductName} with Id:{product.ProductId} has not found");
            }

            _productServcie.Delete(product);

            return NoContent();
        }
        #endregion

        #region Withdraw Product Endpoint
        [HttpPut("WithDraw")]
        public async Task<ActionResult<ApiResult<CreateProductDto>>> WithDrawProduct([FromQuery] WithDrawProducts dto)
        {
            var exist = await _productServcie.GetProductByCode(dto.ProductCode);
            

          //  if (dto.Product_Quantity > exist.Product_Quantity)
               // return BadRequest($"Your request is bigger Than the stock , the sock has {exist.Product_Quantity} of {exist.Product_Name}");

            _productServcie.WithDraw(dto);

            return HandleOperationResult(exist);
        }

        #endregion
    }
}