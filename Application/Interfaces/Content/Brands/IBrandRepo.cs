using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Content.Brands
{
    public interface IBrandRepo
    {
        Task<List<InsuranceCoyEntity>> GetAll();

        Task<InsuranceCoyEntity> GetById(int id);

        Task<InsuranceCoyEntity> Add_Brand(InsuranceCoyEntity model);

        Task<InsuranceCoyEntity> Update_Brand(InsuranceCoyEntity model);

        Task<InsuranceCoyEntity> Delete_Brand(InsuranceCoyEntity model);

        Task<bool> BrandIsExist(string code);



    }
}











