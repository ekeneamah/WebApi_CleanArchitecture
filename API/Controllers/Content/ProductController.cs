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
using Domain.Entities;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productServcie;

        public ProductController(IProduct productServcie)
        {
            _productServcie = productServcie;

        }

        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet("GetAll")]
        public async Task<ActionResult<ProductDtoDetails>> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetAll(pageNumber, pageSize);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return Ok(products);
        }
        #endregion

        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet("GetAllProductsByGroup")]
        public async Task<ActionResult<ProductDtoDetails>> GetProductsByGroup(string group_name,int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetAllProductsByGroup(pageNumber, pageSize,group_name);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return Ok(products);
        }
        #endregion

        #region GetAllProducts by category id Endpoint
        // GET: api/Products
        [HttpGet("GetProductsByCategoryId")]
        public async Task<ActionResult<ProductDtoDetails>> GetProductsByCategoryId(int product_categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetAllProductsByCategory( pageNumber, pageSize, product_categoryId);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return Ok(products);
        }
        #endregion
        #region GetAllProducts by insurance id Endpoint
        // GET: api/Products
        [HttpGet("GetProductsByInsuranceCoyId")]
        public async Task<ActionResult<Product>> GetProductsByInsuranceCoyId(int insuranceCoyId, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetProductsByInsuranceCoyId(pageNumber, pageSize, insuranceCoyId);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return Ok(products);
        }
        #endregion
        #region Get Recommended Products Endpoint
        // GET: api/Products
        [HttpGet("GetRecommendedProducts")]
        public async Task<ActionResult<Product>> GetRecommendedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productServcie.GetRecommendedProducts(pageNumber, pageSize);
            //var paginatedProducts = PaginatedList<ProductDtoDetails>.Create(products, pageNumber, pageSize);

            return Ok(products);
        }
        #endregion
        #region get product details

        // GET: api/Products/5
        [HttpGet("{code}")]
        public async Task<ActionResult<Product>> GetProduct(string code)
        {
            
            var product = await _productServcie.GetByCode(code);

            if (product == null)
            {
                return NotFound($"no ProductEntity with {code} was found");
            }

            return Ok(product);
        }
        #endregion

        #region get product details

        // GET: api/Products/5
        [HttpGet("GetProductDetailById")]
        public async Task<ActionResult<Product>> GetProductDetailById(int product_id)
        {

            var product = await _productServcie.GetDetailsById(product_id);

            if (product == null)
            {
                return NotFound($"no ProductEntity with {product_id} was found");
            }

            return Ok(product);
        }
        #endregion


        #region Create Product Endpoint
        // POST: api/Products
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> PostProduct( CreateProductDto model)
        {

            var isExist = await _productServcie.ProductIsExist(model.ProductCode);

            if (isExist is true)
            {
                return BadRequest("ProductEntity Code already exists");

            }
            else
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    CoyId = model.InsuranceCoyId,
                    CategoryId = model.CategoryId,
                    ProductPrice = model.ProductPrice,
                    ProductQuantity = model.ProductQuantity,
                    ProductCode = model.ProductCode,
                    ProductDescription = model.ProductDescription,
                    ProductGroup = model.ProductGroup,
                    
                };

               return Ok( await _productServcie.Add(product));
            }
           
            //return Ok();
        }
        #endregion

        #region Update Product Endpoint
        // PUT: api/Products/5
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, CreateProductDto model)
        {
            var product = await _productServcie.GetById(id);

            if (product == null)
            {
                return NotFound($"brand:{model.ProductName}  has not found");
            }

            product.ProductName = model.ProductName;
            product.CoyId = model.InsuranceCoyId;
            product.ProductPrice = model.ProductPrice;
            product.ProductQuantity = model.ProductQuantity;
            product.ProductDescription = model.ProductDescription;
            product.CategoryId = model.CategoryId;
            product.ProductCode = model.ProductCode;


            _productServcie.Update(product);

            return Ok(product);
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
        public async Task<IActionResult> WithDrawProduct(WithDrawProducts dto)
        {
            var exist = _productServcie.GetProductByCode(dto.ProductCode);

            if (exist is null)
            {
                return BadRequest($"No Products found with this Code {dto.ProductCode}");

            }

          //  if (dto.Product_Quantity > exist.Product_Quantity)
               // return BadRequest($"Your request is bigger Than the stock , the sock has {exist.Product_Quantity} of {exist.Product_Name}");

            _productServcie.WithDraw(dto);

            return Ok(exist);
        }

        #endregion
    }
}