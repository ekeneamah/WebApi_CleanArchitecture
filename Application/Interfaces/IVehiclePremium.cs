using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehiclePremiumRepository
    {
        Task<IEnumerable<VehiclePremiumEntity>> GetAllVehiclePremiumsAsync();
        Task<VehiclePremiumEntity> GetVehiclePremiumByIdAsync(int id);
        Task AddVehiclePremiumAsync(VehiclePremiumEntity vehiclePremium);
        Task UpdateVehiclePremiumAsync(VehiclePremiumEntity vehiclePremium);
        Task DeleteVehiclePremiumAsync(int id);
    }
}
