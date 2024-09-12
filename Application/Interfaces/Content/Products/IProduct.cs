using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Content.Products
{
    public interface IProduct
    {
        Task<ApiResult<List<ProductDtoDetails>>> GetAll(int pageNumber, int pageSize);
        Task<ApiResult<List<ProductDtoDetails>>> GetAllProductsByCategory(int pageNumber, int pageSize,
            int product_categoryId);
        Task<ApiResult<List<ProductGroupDto>>> GetAllProductsGroup();
        Task<ApiResult<List<ProductDtoDetails>>> GetAllProductsByGroup(int pageNumber, int pageSize,
            string product_groupname);
        Task<Product> GetById(int id);
        Task<ApiResult<ProductDtoDetails>> GetDetailsById(int id);
        Task<ApiResult<ProductDtoDetails>> GetByCode(string code);
        Task<ApiResult<CreateProductDto>> GetProductByCode(string code);
        Task<List<ProductForInsuranceCoyDetailsDto>> ProductForInsuranceCoyDetails(int coyId);

        Task<Product> Add(Product model);

        Product Update(Product model);

        Product Delete(Product model);

        Task<bool> ProductIsExist(string code);
        Task<int> SaveChanges();
        void WithDraw(WithDrawProducts dto);
        Task<ApiResult<List<Product>>> GetProductsByInsuranceCoyId(int pageNumber, int pageSize, int coyId);
        Task<ApiResult<List<Product>>> GetRecommendedProducts(int pageNumber, int pageSize);
    }
}
