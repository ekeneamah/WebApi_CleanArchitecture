using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Content.Products
{
    public interface IProduct
    {
        Task<List<ProductDtoDetails>> GetAll(int pageNumber, int pageSize);
        Task<List<ProductDtoDetails>> GetAllProductsByCategory(int pageNumber, int pageSize, int product_categoryId);
        Task<List<ProductGroupDto>> GetAllProductsGroup();
        Task<List<ProductDtoDetails>> GetAllProductsByGroup(int pageNumber, int pageSize, string product_groupname);
        Task<Product> GetById(int id);
        Task<ProductDtoDetails> GetDetailsById(int id);
        Task<ProductDtoDetails> GetByCode(string code);
        Task<CreateProductDto> GetProductByCode(string code);

        Task<Product> Add(Product model);

        Product Update(Product model);

        Product Delete(Product model);

        Task<bool> ProductIsExist(string code);
        Task<int> SaveChanges();
        void WithDraw(WithDrawProducts dto);
        Task<List<Product>> GetProductsByInsuranceCoyId(int pageNumber, int pageSize, int insuranceCoyId);
        Task<List<Product>> GetRecommendedProducts(int pageNumber, int pageSize);
    }
}
