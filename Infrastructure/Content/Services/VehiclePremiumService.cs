using Application.Interfaces;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Content.Services
{
    public class VehiclePremiumService : IVehiclePremiumRepository
    {
        private readonly AppDbContext _dbContext;

        public VehiclePremiumService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<VehiclePremium>> GetAllVehiclePremiumsAsync()
        {
            return await _dbContext.VehiclePremiums.ToListAsync();
        }

        public async Task<VehiclePremium> GetVehiclePremiumByIdAsync(int id)
        {
            return await _dbContext.VehiclePremiums.FindAsync(id);
        }

        public async Task AddVehiclePremiumAsync(VehiclePremium vehiclePremium)
        {
            await _dbContext.VehiclePremiums.AddAsync(vehiclePremium);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVehiclePremiumAsync(VehiclePremium vehiclePremium)
        {
            _dbContext.Entry(vehiclePremium).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehiclePremiumAsync(int id)
        {
            var vehiclePremium = await _dbContext.VehiclePremiums.FindAsync(id);
            if (vehiclePremium != null)
            {
                _dbContext.VehiclePremiums.Remove(vehiclePremium);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
