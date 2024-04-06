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
        Task<List<InsuranceCoy>> GetAll();

        Task<InsuranceCoy> GetById(int id);

        Task<InsuranceCoy> Add_Brand(InsuranceCoy model);

        Task<InsuranceCoy> Update_Brand(InsuranceCoy model);

        Task<InsuranceCoy> Delete_Brand(InsuranceCoy model);

        Task<bool> BrandIsExist(string code);



    }
}











