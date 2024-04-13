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
        Task<KYCDTO> CreateKYC(KYCDTO kycDto);
        Task<KYCDTO> GetKYCById(int id);
        Task<IEnumerable<KYCDTO>> GetAllKYC();
        Task<KYCDTO> UpdateKYC(int id, KYCDTO kycDto);
        Task<bool> DeleteKYC(int id);
        Task<List<KYCDTO>> GetKYCByUserId(string userid);
    }
}
