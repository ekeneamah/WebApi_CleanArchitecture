using Application.Dtos;
using Application.Interfaces.Content.Products;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

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
              
                 .OrderBy(x => x.Product_Name)
                 .AsNoTracking()
                 .ToListAsync();

            foreach (ProductEntity x in p)
            {
                ProductDtoDetails pd = new()
                {
                    Product_Id = x.Product_Id,
                    Product_Name = x.Product_Name,
                    Coy_Name = await _context.InsuranceCompany.Where(i => i.Coy_Id == x.Coy_Id).Select(n => n.Coy_Name).FirstOrDefaultAsync(),
                    Coy_Id = x.Coy_Id,
                    Category_Name = await _context.Categories.Where(i => i.Categoty_Id == x.Category_Id).Select(n => n.Category_Name).FirstOrDefaultAsync(),

                    Category_Id = x.Category_Id,
                    Product_Price = x.Product_Price,
                    Product_Quantity = x.Product_Quantity,
                    Product_Code = x.Product_Code,
                    Product_Group = x.Product_Group
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
                .Where(c => c.Product_Code == code)               
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            ProductDtoDetails pd =  new() 
              {
                  Product_Id = x.Product_Id,
                  Product_Name = x.Product_Name,
                  Coy_Name = await _context.InsuranceCompany.Where(i => i.Coy_Id == x.Coy_Id).Select(n => n.Coy_Name).FirstOrDefaultAsync(),
                  Coy_Id = x.Coy_Id,
                  Category_Name = await _context.Categories.Where(i => i.Categoty_Id == x.Category_Id).Select(n => n.Category_Name).FirstOrDefaultAsync(),
                  Category_Id = x.Category_Id,
                  Product_Price = x.Product_Price,
                  Product_Quantity = x.Product_Quantity,
                  Product_Code = x.Product_Code,
                  Product_description = x.Product_Description
              };

            return pd;
        }
        #endregion

        #region GetProductByCode
        // return entity/model : (ProductEntity)
        public async Task<ProductDto> GetProductByCode(string code)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Product_Code == code);
            ProductDto pd = new ProductDto()
            {
                InsuranceCoy_id = product.Coy_Id,
                Product_Name = product.Product_Name,
                Product_Code = product.Product_Code,
                Category_Id = product.Category_Id,
                Product_Price = product.Product_Price,
                Product_Quantity = product.Product_Quantity,

            };

            return pd;
        }
        #endregion

        #region GetDetailsById
        public async Task<ProductDtoDetails> GetDetailsById(int id)
        {
            ProductEntity x = await _context.Products.FindAsync(id);
            ProductDtoDetails pd = new()
            {
                Product_Id = x.Product_Id,
                Product_Name = x.Product_Name,
                InsuranceCoy = await _context.InsuranceCompany.Where(i => i.Coy_Id == x.Coy_Id).FirstOrDefaultAsync(),
                Coy_Id = x.Coy_Id,
                Product_Category = await _context.Categories.Where(i => i.Categoty_Id == x.Category_Id).FirstOrDefaultAsync(),

                Category_Id = x.Category_Id,
                Product_Price = x.Product_Price,
                Product_Quantity = x.Product_Quantity,
                Product_Code = x.Product_Code,
                Product_Group = x.Product_Group
            };
            return pd;
        }
        #endregion

        #region GetById
        public async Task<ProductEntity> GetById(int id)
        {
            ProductEntity x =  await _context.Products.FindAsync(id);
           
            return x;
        }
        #endregion

        #region Add 
        public async Task<ProductEntity> Add(ProductEntity model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Update
        public ProductEntity Update(ProductEntity model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Delete
        public ProductEntity Delete(ProductEntity model)
        {
            _context.Products.Remove(model);
            _context.SaveChangesAsync();

            return model;
        }
        #endregion

        #region ProductIsExist
        public async Task<bool> ProductIsExist(string code)
        {
            return await _context.Products.AnyAsync(p => p.Product_Code == code);
        }
        #endregion

        #region WithDraw
        public async void WithDraw(WithDrawProducts dto)
        {
            ProductDto product = await GetProductByCode(dto.Product_Code);

            product.Product_Quantity = dto.Product_Quantity - product.Product_Quantity;

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
            var p = await _context.Products.Where(g => g.Category_Id == product_categoryId)
                 
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)                 
                 .OrderBy(x => x.Product_Name)
                 .AsNoTracking()
                 .ToListAsync();
            List<ProductDtoDetails> result = new List<ProductDtoDetails>(); 

            foreach (ProductEntity x in p)
            {
                ProductDtoDetails pd = new()
                {
                    Product_Id = x.Product_Id,
                    Product_Name = x.Product_Name,
                    InsuranceCoy = await _context.InsuranceCompany.Where(i => i.Coy_Id == x.Coy_Id).FirstOrDefaultAsync(),
                    Coy_Id = x.Coy_Id,
                    Product_Category = await _context.Categories.Where(i => i.Categoty_Id == x.Category_Id).FirstOrDefaultAsync(),

                    Category_Id = x.Category_Id,
                    Product_Price = x.Product_Price,
                    Product_Quantity = x.Product_Quantity,
                    Product_Code = x.Product_Code,
                    Product_Group = x.Product_Group
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
            .GroupBy(p => p.Product_Group)
            .Select(g => new ProductGroupDto
            {
                GroupName = (string)g.Key,
                Count = g.Count(),
                AveragePrice = g.Average(p => p.Product_Price)
            })
            .ToListAsync();
            return productGroups;
          }
        #endregion
        #region GetAllProductsByGroup
        public async Task<List<ProductDtoDetails>> GetAllProductsByGroup(int pageNumber, int pageSize, string product_groupname)
        {
            var p = await _context.Products.Where(g=>g.Product_Group== product_groupname)
                 
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                
                 .OrderBy(x => x.Product_Name)
                 .AsNoTracking()
                 .ToListAsync();

            List<ProductDtoDetails> result = new List<ProductDtoDetails>();

            foreach (ProductEntity x in p)
            {
                ProductDtoDetails pd = new()
                {
                    Product_Id = x.Product_Id,
                    Product_Name = x.Product_Name,
                    InsuranceCoy = await _context.InsuranceCompany.Where(i => i.Coy_Id == x.Coy_Id).FirstOrDefaultAsync(),
                    Coy_Id = x.Coy_Id,
                    Product_Category = await _context.Categories.Where(i => i.Categoty_Id == x.Category_Id).FirstOrDefaultAsync(),

                    Category_Id = x.Category_Id,
                    Product_Price = x.Product_Price,
                    Product_Quantity = x.Product_Quantity,
                    Product_Code = x.Product_Code,
                    Product_Group = x.Product_Group
                };
                result.Add(pd);
            }

            return result;
        }
        #endregion

    }
}