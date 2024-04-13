using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces.Content.Brands
{
    public interface IInsuranceCoy
    {
        Task<List<InsuranceCoyDTO>> GetAll();

        Task<InsuranceCoyDTO> GetById(int id);

        Task<InsuranceCoyDTO> Add_Coy(InsuranceCoyDTO model);

        Task<int> Update_Coy(InsuranceCoyDTO model);

        Task<InsuranceCoyEntity> Delete_Coy(InsuranceCoyDTO model);

        Task<bool> CoyIsExist(string code);

    }
}
