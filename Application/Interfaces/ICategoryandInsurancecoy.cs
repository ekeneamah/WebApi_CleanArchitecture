using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryandInsurancecoy
    {
        // Create operation
        void Add(CategoryandInsurancecoy item);

        // Read operation
        CategoryandInsurancecoy GetById(int id);
        IEnumerable<CategoryandInsurancecoy> GetAll();
        Task<List<CategoryandInsurancecoyDto>> GetByCategoryId(int categoryId);
        Task<List<CategoryandInsurancecoyDto>> GetByInsuranceCoyId(int insuranceCoyId);

        // Update operation
        void Update(CategoryandInsurancecoy item);

        // Delete operation
        void Delete(int id);
    }
}
