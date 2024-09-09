using Application.Dtos;
using Application.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;

namespace API.Controllers.Content
{
    [Route("api/kycs")]
[ApiController]
public class KYCController : BaseController
{
    private readonly IKYC _kycService;
        private readonly UserManager<AppUser> _userManager;

        public KYCController(IKYC kycService,UserManager<AppUser> userManager)
    {
        _kycService = kycService;
            _userManager = userManager;
    }


    [HttpPost]
    public async Task<ActionResult<ApiResult<Kycdto>>> CreateKYC([FromBody]Kycdto kycDto)
        {
        
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return HandleOperationResult( ApiResult<Kycdto>.Failed("Invalid User"));
            kycDto.UserId = user.Id;
            var createdKYC = await _kycService.CreateKYC(kycDto);
        return HandleOperationResult(createdKYC);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<Kycdto>>> GetKYCById(int id)
    {
        var kycDto = await _kycService.GetKYCById(id);
     
        return HandleOperationResult(kycDto);
    }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResult<List<Kycdto>>>> GetKYCByUserId()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return HandleOperationResult( ApiResult<List<Kycdto>>.Failed("Invalid User"));
            var kycDto = await _kycService.GetKYCByUserId(user.Id);

            return HandleOperationResult(kycDto);
        }

        [HttpGet]
    public async Task<ActionResult<ApiResult<List<Kycdto>>>> GetAllKYC()
    {
        var kycDtos = await _kycService.GetAllKYC();
        return HandleOperationResult(kycDtos);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResult<Kycdto>>> UpdateKYC(int id, [FromBody] Kycdto kycDto)
    {
      
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return HandleOperationResult(ApiResult<Kycdto>.Failed("Invalid User"));
            kycDto.UserId = user.Id;
            var updatedKYC = await _kycService.UpdateKYC(id, kycDto);
            return HandleOperationResult(updatedKYC);
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