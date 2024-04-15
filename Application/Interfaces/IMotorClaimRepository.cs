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
        Task<MotorClaim> CreateMotorClaim(MotorClaim motorClaim);
        Task<MotorClaim> GetMotorClaimById(int id);
        Task<IEnumerable<MotorClaim>> GetAllMotorClaims(string userid);
        Task UpdateMotorClaim(MotorClaim motorClaim);
        Task DeleteMotorClaim(int id);
    }
}
