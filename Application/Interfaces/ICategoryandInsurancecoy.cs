using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Application.Interfaces
{
    public interface ICategoryandInsurancecoy
    {
        // Create operation
        void Add(CategoryandInsurancecoy item);

        // Read operation
        ApiResult<CategoryandInsurancecoy> GetById(int id);
        ApiResult<List<CategoryandInsurancecoy>> GetAll();
        Task<ApiResult<List<CategoryandInsurancecoyDto>>> GetByCategoryId(int categoryId);
        Task<ApiResult<List<CategoryandInsurancecoyDto>>> GetByInsuranceCoyId(int coyId);

        // Update operation
        void Update(CategoryandInsurancecoy item);

        // Delete operation
        void Delete(int id);
    }
}
