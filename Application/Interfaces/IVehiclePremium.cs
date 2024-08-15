using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVehiclePremiumRepository
    {
        Task<IEnumerable<VehiclePremium>> GetAllVehiclePremiumsAsync();
        Task<VehiclePremium> GetVehiclePremiumByIdAsync(int id);
        Task AddVehiclePremiumAsync(VehiclePremium vehiclePremium);
        Task UpdateVehiclePremiumAsync(VehiclePremium vehiclePremium);
        Task DeleteVehiclePremiumAsync(int id);
    }
}
