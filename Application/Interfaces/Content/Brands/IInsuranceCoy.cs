using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Content.Brands
{
    public interface IInsuranceCoy
    {
        Task<List<InsuranceCoyDto>> GetAll(int pageNumber, int pageSize);

        Task<InsuranceCoyDto> GetById(int id);

        Task<InsuranceCoyDto> Add_Coy(InsuranceCoyDto model);

        Task<int> Update_Coy(InsuranceCoyDto model);

        Task<InsuranceCoy> Delete_Coy(InsuranceCoyDto model);

        Task<bool> CoyIsExist(string code);

    }
}
