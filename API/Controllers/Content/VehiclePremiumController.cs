using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclePremiumsController : BaseController
    {
        private readonly IVehiclePremiumRepository _vehiclePremiumRepository;

        public VehiclePremiumsController(IVehiclePremiumRepository vehiclePremiumRepository)
        {
            _vehiclePremiumRepository = vehiclePremiumRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<VehiclePremium>>>> GetAllVehiclePremiums()
        {
           
                var vehiclePremiums = await _vehiclePremiumRepository.GetAllVehiclePremiumsAsync();
                return HandleOperationResult(vehiclePremiums);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<VehiclePremium>>> GetVehiclePremiumById(int id)
        {
                var vehiclePremium = await _vehiclePremiumRepository.GetVehiclePremiumByIdAsync(id);
              
                return HandleOperationResult(vehiclePremium);
          
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<VehiclePremium>>> AddVehiclePremium(VehiclePremium vehiclePremium)
        {
               return HandleOperationResult(await _vehiclePremiumRepository.GetVehiclePremiumByIdAsync(vehiclePremium.Id));
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehiclePremium(int id, VehiclePremium vehiclePremium)
        {
            if (id != vehiclePremium.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _vehiclePremiumRepository.UpdateVehiclePremiumAsync(vehiclePremium);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiclePremium(int id)
        {
            try
            {
                await _vehiclePremiumRepository.DeleteVehiclePremiumAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}