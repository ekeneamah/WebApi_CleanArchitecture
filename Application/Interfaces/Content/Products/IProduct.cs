using Application.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Content.Products
{
    public interface IProduct
    {
        Task<List<ProductDtoDetails>> GetAll(int pageNumber, int pageSize);
        Task<List<ProductDtoDetails>> GetAllProductsByCategory(int pageNumber, int pageSize, int product_categoryId);
        Task<List<ProductGroupDto>> GetAllProductsGroup();
        Task<List<ProductDtoDetails>> GetAllProductsByGroup(int pageNumber, int pageSize, string product_groupname);
        Task<ProductEntity> GetById(int id);
        Task<ProductDtoDetails> GetDetailsById(int id);
        Task<ProductDtoDetails> GetByCode(string code);
        Task<ProductDto> GetProductByCode(string code);

        Task<ProductEntity> Add(ProductEntity model);

        ProductEntity Update(ProductEntity model);

        ProductEntity Delete(ProductEntity model);

        Task<bool> ProductIsExist(string code);
        Task<int> SaveChanges();
        void WithDraw(WithDrawProducts dto);
    }
}
