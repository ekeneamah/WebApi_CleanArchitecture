using Application.Dtos;
using Application.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace API.Controllers.Content
{
    [Route("api/[controller]")]
[ApiController]
public class KYCController : ControllerBase
{
    private readonly IKYC _kycService;
        private readonly UserManager<AppUser> _userManager;

        public KYCController(IKYC kycService,UserManager<AppUser> userManager)
    {
        _kycService = kycService;
            _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult<KYCDTO>> CreateKYC(KYCDTO kycDto)
        {
            if (kycDto.IdentityType != "NIN")
            {


                // Check if expiry date is greater than issued date
                if (kycDto.FromExpiryDate > kycDto.ToExpiryDate)
                {
                    return BadRequest("Expiry date must be greater than issued date.");
                }

                // Check if expiry date has not passed
                if (DateTime.Today > kycDto.ToExpiryDate)
                {
                    return BadRequest("Identity has expired.");
                }
                // Check if expiry date has not passed
                if (DateTime.Today < kycDto.FromExpiryDate)
                {
                    return BadRequest("Identity has expired.");
                }
            }
            else // Check if expiry date has not passed
                    if (DateTime.Today < kycDto.FromExpiryDate)
            {
                return BadRequest("Date is invalid.");
            }
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            kycDto.UserId = user.Id;
            var createdKYC = await _kycService.CreateKYC(kycDto);
        return CreatedAtAction(nameof(GetKYCById), new { id = createdKYC.Id }, createdKYC);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KYCDTO>> GetKYCById(int id)
    {
        var kycDto = await _kycService.GetKYCById(id);
        if (kycDto == null)
        {
            return NotFound();
        }
        return kycDto;
    }

        [HttpGet("GetKycByUserId")]
        public async Task<ActionResult<List<KYCDTO>>> GetKYCByUserId()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            var kycDto = await _kycService.GetKYCByUserId(user.Id);
            if (kycDto == null)
            {
                return NotFound();
            }

            return kycDto;
        }

        [HttpGet]
    public async Task<ActionResult<IEnumerable<KYCDTO>>> GetAllKYC()
    {
        var kycDtos = await _kycService.GetAllKYC();
        return Ok(kycDtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateKYC(int id, KYCDTO kycDto)
    {
        if (id != kycDto.Id)
        {
            return BadRequest();
        }
            if (kycDto.IdentityType != "NIN")
            {


                // Check if expiry date is greater than issued date
                if (kycDto.FromExpiryDate > kycDto.ToExpiryDate)
                {
                    return BadRequest("Expiry date must be greater than issued date.");
                }

                // Check if expiry date has not passed
                if (DateTime.Today > kycDto.ToExpiryDate)
                {
                    return BadRequest("Identity has expired.");
                }
               
            }
            else // Check if expiry date has not passed
                if (DateTime.Today < kycDto.FromExpiryDate)
            {
                return BadRequest("Date is invalid.");
            }
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            kycDto.UserId = user.Id;
            var updatedKYC = await _kycService.UpdateKYC(id, kycDto);
        if (updatedKYC == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKYC(int id)
    {
        var result = await _kycService.DeleteKYC(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
}