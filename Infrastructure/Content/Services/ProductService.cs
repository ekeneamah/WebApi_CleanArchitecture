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
            var product = await _context.Products
                 .Include(r => r.InsuranceCoy)
                 .Include(r => r.Category)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                 .Select(x => new ProductDtoDetails
                 {
                     Product_Id = x.Product_Id,
                     Product_Name = x.Product_Name,
                     Coy_Name = x.InsuranceCoy.Coy_Name,
                     Coy_Id = x.Coy_Id,
                     Category_Name = x.Category.Category_Name,
                     Category_Id = x.Category_Id,
                     Product_Price = x.Product_Price,
                     Product_Quantity = x.Product_Quantity,
                     Product_Code = x.Product_Code,
                     Product_Group = x.Product_Group
                 })
                 .OrderBy(x => x.Product_Name)
                 .AsNoTracking()
                 .ToListAsync();

            return product;
        }

        #endregion

        # region GetByCode
        //return Dto : (ProductDtoDetails)
        public async Task<ProductDtoDetails> GetByCode(string code)
        {
            var product = await _context.Products
                .Where(c => c.Product_Code == code)
                 .Include(r => r.InsuranceCoy)
                 .Select(x => new ProductDtoDetails
                 {
                     Product_Id = x.Product_Id,
                     Product_Name = x.Product_Name,
                     Coy_Name = x.InsuranceCoy.Coy_Name,
                     Coy_Id = x.Coy_Id,
                     Category_Name = x.Category.Category_Name,
                     Category_Id = x.Category_Id,
                     Product_Price = x.Product_Price,
                     Product_Quantity = x.Product_Quantity,
                     Product_Code = x.Product_Code,
                     Product_description = x.Product_Description
                 })
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            return product;
        }
        #endregion

        #region GetProductByCode
        // return entity/model : (Product)
        public async Task<ProductDto> GetProductByCode(string code)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Product_Code == code);
            ProductDto pd = new ProductDto()
            {
                Brand_Id = product.Coy_Id,
                Product_Name = product.Product_Name,
                Product_Code = product.Product_Code,
                Category_Id = product.Category_Id,
                Product_Price = product.Product_Price,
                Product_Quantity = product.Product_Quantity,

            };

            return pd;
        }
        #endregion

        #region GetById
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
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
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion
        #region GetAllProductsByCategory
        public async Task<List<ProductDtoDetails>> GetAllProductsByCategory(int pageNumber, int pageSize, int product_categoryId)
        {
            var product = await _context.Products.Where(g => g.Category_Id == product_categoryId)
                 .Include(r => r.InsuranceCoy)
            .Include(r => r.Category)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                 .Select(x => new ProductDtoDetails
                 {
                     Product_Id = x.Product_Id,
                     Product_Name = x.Product_Name,
                     Coy_Name = x.InsuranceCoy.Coy_Name,
                     Coy_Id = x.Coy_Id,
                     Category_Name = x.Category.Category_Name,
                     Category_Id = x.Category_Id,
                     Product_Price = x.Product_Price,
                     Product_Quantity = x.Product_Quantity,
                     Product_Code = x.Product_Code,
                     Product_Group = x.Product_Group
                 })
                 .OrderBy(x => x.Product_Name)
                 .AsNoTracking()
                 .ToListAsync();

            return product;
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
            var product = await _context.Products.Where(g=>g.Product_Group== product_groupname)
                 .Include(r => r.InsuranceCoy)
            .Include(r => r.Category)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                 .Select(x => new ProductDtoDetails
                 {
                     Product_Id = x.Product_Id,
                     Product_Name = x.Product_Name,
                     Coy_Name = x.InsuranceCoy.Coy_Name,
                     Coy_Id = x.Coy_Id,
                     Category_Name = x.Category.Category_Name,
                     Category_Id = x.Category_Id,
                     Product_Price = x.Product_Price,
                     Product_Quantity = x.Product_Quantity,
                     Product_Code = x.Product_Code,
                     Product_Group = x.Product_Group
                 })
                 .OrderBy(x => x.Product_Name)
                 .AsNoTracking()
                 .ToListAsync();

            return product;
        }
        #endregion

    }
}