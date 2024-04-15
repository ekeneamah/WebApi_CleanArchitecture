using API.helper;
using Application.Dtos;
using Application.Interfaces.Content.Products;
using Domain;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Drawing.Printing;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<int>> PostProduct( ProductDto model)
        {

            var isExist = await _productServcie.ProductIsExist(model.Product_Code);

            if (isExist is true)
            {
                return BadRequest("ProductEntity Code already exists");

            }
            else
            {
                var product = new Product
                {
                    Product_Name = model.Product_Name,
                    Coy_Id = model.InsuranceCoy_id,
                    Category_Id = model.Category_Id,
                    Product_Price = model.Product_Price,
                    Product_Quantity = model.Product_Quantity,
                    Product_Code = model.Product_Code,
                    Product_Description = model.Product_Description,
                    Product_Group = model.Product_Group,
                    
                };

               return Ok( await _productServcie.Add(product));
            }
           
            //return Ok();
        }
        #endregion

        #region Update Product Endpoint
        // PUT: api/Products/5
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, ProductDto model)
        {
            var product = await _productServcie.GetById(id);

            if (product == null)
            {
                return NotFound($"brand:{product.Product_Name} with Id:{product.Product_Id} has not found");
            }

            product.Product_Name = model.Product_Name;
            product.Coy_Id = model.InsuranceCoy_id;
            product.Coy_Id = model.InsuranceCoy_id;
            product.Product_Price = model.Product_Price;
            product.Product_Quantity = model.Product_Quantity;

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
                return NotFound($"brand:{product.Product_Name} with Id:{product.Product_Id} has not found");
            }

            _productServcie.Delete(product);

            return NoContent();
        }
        #endregion

        #region Withdraw Product Endpoint
        [HttpPut("WithDraw")]
        public async Task<IActionResult> WithDrawProduct(WithDrawProducts dto)
        {
            var exist = _productServcie.GetProductByCode(dto.Product_Code);

            if (exist is null)
            {
                return BadRequest($"No Products found with this Code {dto.Product_Code}");

            }

          //  if (dto.Product_Quantity > exist.Product_Quantity)
               // return BadRequest($"Your request is bigger Than the stock , the sock has {exist.Product_Quantity} of {exist.Product_Name}");

            _productServcie.WithDraw(dto);

            return Ok(exist);
        }

        #endregion
    }
}