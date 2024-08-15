using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IKYC
    {
        Task<Kycdto> CreateKYC(Kycdto kycDto);
        Task<Kycdto> GetKYCById(int id);
        Task<IEnumerable<Kycdto>> GetAllKYC();
        Task<Kycdto> UpdateKYC(int id, Kycdto kycDto);
        Task<bool> DeleteKYC(int id);
        Task<List<Kycdto>> GetKYCByUserId(string userid);
    }
}
