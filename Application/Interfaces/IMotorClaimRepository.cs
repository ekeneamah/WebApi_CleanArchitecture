using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMotorClaimRepository
    {
        Task<ApiResult<MotorClaim>> CreateMotorClaim(MotorClaim motorClaim);
        Task<ApiResult<MotorClaim>> GetMotorClaimById(int id);
        Task<ApiResult<List<MotorClaim>>> GetAllMotorClaims(string userid);
        Task UpdateMotorClaim(MotorClaim motorClaim);
        Task DeleteMotorClaim(int id);
    }
}
