using Application.Dtos;
using Application.Interfaces.Content.Products;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Domain.Entities;

namespace Infrastructure.Content.Services
{
    public class ProductService : IProduct
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        #region GetAll
        public async Task<List<ProductDtoDetails>> GetAll(int pageNumber, int pageSize)
        {
            List<ProductDtoDetails> result = new List<ProductDtoDetails>();
            var p = await _context.Products
                 
                 
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
              
                 .OrderBy(x => x.ProductName)
                 .AsNoTracking()
                 .ToListAsync();

            foreach (Product x in p)
            {
                ProductDtoDetails pd = new()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    CoyName = await _context.InsuranceCompany.Where(i => i.CoyId == x.CoyId).Select(n => n.CoyName).FirstOrDefaultAsync(),
                    CoyId = x.CoyId,
                    CategoryName = await _context.Categories.Where(i => i.CategoryId == x.CategoryId).Select(n => n.CategoryName).FirstOrDefaultAsync(),

                    CategoryId = x.CategoryId,
                    ProductPrice = x.ProductPrice,
                    ProductQuantity = x.ProductQuantity,
                    ProductCode = x.ProductCode,
                    ProductGroup = x.ProductGroup
                };
                result.Add(pd);
            }

            return result;
        }

        #endregion

        # region GetByCode
        //return Dto : (ProductDtoDetails)
        public async Task<ProductDtoDetails> GetByCode(string code)
        {
            var x = await _context.Products
                .Where(c => c.ProductCode == code)               
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            ProductDtoDetails pd =  new() 
              {
                  ProductId = x.ProductId,
                  ProductName = x.ProductName,
                  CoyName = await _context.InsuranceCompany.Where(i => i.CoyId == x.CoyId).Select(n => n.CoyName).FirstOrDefaultAsync(),
                  CoyId = x.CoyId,
                  CategoryName = await _context.Categories.Where(i => i.CategoryId == x.CategoryId).Select(n => n.CategoryName).FirstOrDefaultAsync(),
                  CategoryId = x.CategoryId,
                  ProductPrice = x.ProductPrice,
                  ProductQuantity = x.ProductQuantity,
                  ProductCode = x.ProductCode,
                  ProductDescription = x.ProductDescription
              };

            return pd;
        }
        #endregion

        #region GetProductByCode
        // return entity/model : (ProductEntity)
        public async Task<CreateProductDto> GetProductByCode(string code)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductCode == code);
            CreateProductDto pd = new CreateProductDto()
            {
                InsuranceCoyId = product.CoyId,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                CategoryId = product.CategoryId,
                ProductPrice = product.ProductPrice,
                ProductQuantity = product.ProductQuantity,

            };

            return pd;
        }
        #endregion

        #region GetDetailsById
        public async Task<ProductDtoDetails> GetDetailsById(int id)
        {
            Product x = await _context.Products.FindAsync(id);
            ProductDtoDetails pd = new()
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                InsuranceCoy = await _context.InsuranceCompany.Where(i => i.CoyId == x.CoyId).FirstOrDefaultAsync(),
                CoyId = x.CoyId,
                ProductCategory = await _context.Categories.Where(i => i.CategoryId == x.CategoryId).FirstOrDefaultAsync(),

                CategoryId = x.CategoryId,
                ProductPrice = x.ProductPrice,
                ProductQuantity = x.ProductQuantity,
                ProductCode = x.ProductCode,
                ProductGroup = x.ProductGroup
            };
            return pd;
        }
        #endregion

        #region GetById
        public async Task<Product> GetById(int id)
        {
            Product x =  await _context.Products.FindAsync(id);
           
            return x;
        }
        #endregion

        #region Add 
        public async Task<Product> Add(Product model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Update
        public Product Update(Product model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Delete
        public Product Delete(Product model)
        {
            _context.Products.Remove(model);
            _context.SaveChangesAsync();

            return model;
        }
        #endregion

        #region ProductIsExist
        public async Task<bool> ProductIsExist(string code)
        {
            return await _context.Products.AnyAsync(p => p.ProductCode == code);
        }
        #endregion

        #region WithDraw
        public async void WithDraw(WithDrawProducts dto)
        {
            CreateProductDto createProduct = await GetProductByCode(dto.ProductCode);

            createProduct.ProductQuantity = dto.ProductQuantity - createProduct.ProductQuantity;

            SaveChanges();
        }
        #endregion

        #region SaveChanges
        public async Task<int> SaveChanges()
        {
           return await _context.SaveChangesAsync();
        }
        #endregion
        #region GetAllProductsByCategory
        public async Task<List<ProductDtoDetails>> GetAllProductsByCategory(int pageNumber, int pageSize, int product_categoryId)
        {
            var p = await _context.Products.Where(g => g.CategoryId == product_categoryId)
                 
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)                 
                 .OrderBy(x => x.ProductName)
                 .AsNoTracking()
                 .ToListAsync();
            List<ProductDtoDetails> result = new List<ProductDtoDetails>(); 

            foreach (Product x in p)
            {
                ProductDtoDetails pd = new()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    InsuranceCoy = await _context.InsuranceCompany.Where(i => i.CoyId == x.CoyId).FirstOrDefaultAsync(),
                    CoyId = x.CoyId,
                    ProductCategory = await _context.Categories.Where(i => i.CategoryId == x.CategoryId).FirstOrDefaultAsync(),

                    CategoryId = x.CategoryId,
                    ProductPrice = x.ProductPrice,
                    ProductQuantity = x.ProductQuantity,
                    ProductCode = x.ProductCode,
                    ProductGroup = x.ProductGroup
                };
                result.Add(pd);
            }

            return result;
        }
        #endregion
        #region GetAllProductsGroup

        public async Task<List<ProductGroupDto>> GetAllProductsGroup()
        {
            var productGroups = await _context.Products
            .GroupBy(p => p.ProductGroup)
            .Select(g => new ProductGroupDto
            {
                GroupName = (string)g.Key,
                Count = g.Count(),
                AveragePrice = g.Average(p => p.ProductPrice)
            })
            .ToListAsync();
            return productGroups;
          }
        #endregion
        #region GetAllProductsByGroup
        public async Task<List<ProductDtoDetails>> GetAllProductsByGroup(int pageNumber, int pageSize, string product_groupname)
        {
            var p = await _context.Products.Where(g=>g.ProductGroup== product_groupname)
                 
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                
                 .OrderBy(x => x.ProductName)
                 .AsNoTracking()
                 .ToListAsync();

            List<ProductDtoDetails> result = new List<ProductDtoDetails>();

            foreach (Product x in p)
            {
                ProductDtoDetails pd = new()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    InsuranceCoy = await _context.InsuranceCompany.Where(i => i.CoyId == x.CoyId).FirstOrDefaultAsync(),
                    CoyId = x.CoyId,
                    ProductCategory = await _context.Categories.Where(i => i.CategoryId == x.CategoryId).FirstOrDefaultAsync(),

                    CategoryId = x.CategoryId,
                    ProductPrice = x.ProductPrice,
                    ProductQuantity = x.ProductQuantity,
                    ProductCode = x.ProductCode,
                    ProductGroup = x.ProductGroup
                };
                result.Add(pd);
            }

            return result;
        }
        #endregion
        #region get products by insurance coy id

        public async Task<List<Product>> GetProductsByInsuranceCoyId(int pageNumber, int pageSize, int insuranceCoyId)
        {
          return  await _context.Products.Where(g => g.CoyId == insuranceCoyId)

                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                 .OrderBy(x => x.ProductName)
                 .AsNoTracking()
                 .ToListAsync();
        }
        #endregion

        #region get products by insurance coy id

        public async Task<List<Product>> GetRecommendedProducts(int pageNumber, int pageSize)
        {
            return await _context.Products.Where(g => g.IsRecommended==true)

                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                   .OrderBy(x => x.ProductName)
                   .AsNoTracking()
                   .ToListAsync();
        }
        #endregion

    }
}