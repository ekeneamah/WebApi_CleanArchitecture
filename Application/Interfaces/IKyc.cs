using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Application.Interfaces
{
    public interface IKYC
    {
        Task<ApiResult<Kycdto>> CreateKYC(Kycdto kycDto);
        Task<ApiResult<Kycdto>> GetKYCById(int id);
        Task<ApiResult<List<Kycdto>>> GetAllKYC();
        Task<ApiResult<Kycdto>> UpdateKYC(int id, Kycdto kycDto);
        Task<bool> DeleteKYC(int id);
        Task<ApiResult<List<Kycdto>>> GetKYCByUserId(string userid);
    }
}
