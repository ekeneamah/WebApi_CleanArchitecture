using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVehiclePremiumRepository
    {
        Task<ApiResult<List<VehiclePremium>>> GetAllVehiclePremiumsAsync();
        Task<ApiResult<VehiclePremium>> GetVehiclePremiumByIdAsync(int id);
        Task AddVehiclePremiumAsync(VehiclePremium vehiclePremium);
        Task UpdateVehiclePremiumAsync(VehiclePremium vehiclePremium);
        Task DeleteVehiclePremiumAsync(int id);
    }
}
