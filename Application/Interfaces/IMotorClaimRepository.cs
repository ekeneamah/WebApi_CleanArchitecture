using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMotorClaimRepository
    {
        Task<MotorClaimEntity> CreateMotorClaim(MotorClaimEntity motorClaim);
        Task<MotorClaimEntity> GetMotorClaimById(int id);
        Task<IEnumerable<MotorClaimEntity>> GetAllMotorClaims(string userid);
        Task UpdateMotorClaim(MotorClaimEntity motorClaim);
        Task DeleteMotorClaim(int id);
    }
}
