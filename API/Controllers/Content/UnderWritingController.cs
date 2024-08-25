using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Common;
using Application.Dtos.UnderWriting;
using Application.Interfaces.Content.UnderWriting;
using Domain.Entities;

namespace API.Controllers.Content
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UnderWritingController : BaseController
    {
        private readonly IUnderWritingService _underWritingService;

        public UnderWritingController(IUnderWritingService underWritingService)
        {
            _underWritingService = underWritingService;
        }

        [HttpPost("ProductUnderWritingForm")]

        public async Task<ActionResult<ApiResult<UnderWritingForm>>> CreateProductUnderWritingForm([FromBody]ProductUnderWritingDto model)
        {
                var result = await _underWritingService.CreateProductUnderWritingFormAsync(model);
                return HandleOperationResult(result);
            
        }

        [HttpGet("ProductUnderWritingForm")]

        public async Task<ActionResult<ApiResult<UnderWritingForm>>> GetProductUnderWritingForm(
            [Required] int? productId)
        {
            var result = await _underWritingService.GetProductUnderWritingFormAsync(productId.GetValueOrDefault());
            return HandleOperationResult(result);
        }

    }
}