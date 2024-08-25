using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Content.Brands
{
    public interface IInsuranceCoy
    {
        Task<ApiResult<List<InsuranceCoyDto>>> GetAll(int pageNumber, int pageSize);

        Task<ApiResult<InsuranceCoyDetailDto>> GetInsuranceCoyDetailById(int id);
        Task<InsuranceCoy> GetByInsuranceCoyId(int id);

        Task<ApiResult<InsuranceCoyDto>> Add_Coy(InsuranceCoyDto model);

        Task<int> Update_Coy(InsuranceCoy model);

        Task<ApiResult<InsuranceCoy>> Delete_Coy(InsuranceCoy model);

        Task<bool> CoyIsExist(string code);

    }
}
