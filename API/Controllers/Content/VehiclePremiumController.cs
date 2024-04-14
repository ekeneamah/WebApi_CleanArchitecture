using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclePremiumsController : ControllerBase
    {
        private readonly IVehiclePremiumRepository _vehiclePremiumRepository;

        public VehiclePremiumsController(IVehiclePremiumRepository vehiclePremiumRepository)
        {
            _vehiclePremiumRepository = vehiclePremiumRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiclePremiumEntity>>> GetAllVehiclePremiums()
        {
            try
            {
                var vehiclePremiums = await _vehiclePremiumRepository.GetAllVehiclePremiumsAsync();
                return Ok(vehiclePremiums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiclePremiumEntity>> GetVehiclePremiumById(int id)
        {
            try
            {
                var vehiclePremium = await _vehiclePremiumRepository.GetVehiclePremiumByIdAsync(id);
                if (vehiclePremium == null)
                {
                    return NotFound();
                }
                return Ok(vehiclePremium);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehiclePremiumEntity>> AddVehiclePremium(VehiclePremiumEntity vehiclePremium)
        {
            try
            {
                await _vehiclePremiumRepository.AddVehiclePremiumAsync(vehiclePremium);
                return CreatedAtAction(nameof(GetVehiclePremiumById), new { id = vehiclePremium.Id }, vehiclePremium);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehiclePremium(int id, VehiclePremiumEntity vehiclePremium)
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