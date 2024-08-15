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
        Task<List<InsuranceCoyDTO>> GetAll(int pageNumber, int pageSize);

        Task<InsuranceCoyDTO> GetById(int id);

        Task<InsuranceCoyDTO> Add_Coy(InsuranceCoyDTO model);

        Task<int> Update_Coy(InsuranceCoyDTO model);

        Task<InsuranceCoy> Delete_Coy(InsuranceCoyDTO model);

        Task<bool> CoyIsExist(string code);

    }
}
