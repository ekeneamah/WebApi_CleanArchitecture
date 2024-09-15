using Application.Dtos.UnderWriting;
using Application.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FormSubmissionController : ControllerBase
    {
        private readonly ICornerStoneTravelService _cornerStoneTravelService;

        public FormSubmissionController(ICornerStoneTravelService _cornerStoneTravelService)
        {
            _cornerStoneTravelService = _cornerStoneTravelService;
        }

        [HttpPost("travel-policy-amount")]
        public async Task<ActionResult<ApiResult<TravelPollicyResponseDto>>> SubmitForm([FromBody] FormBody formBody)
        {
            // Validate form body (optional but recommended)
            if (formBody == null || !formBody.GlobalFields.Any())
            {
                return ApiResult<TravelPollicyResponseDto>.Failed("Form data is invalid.");
            }

            // Call the service to validate and submit the form
            var result = await _cornerStoneTravelService.GetPolicyAmountAsync(formBody);

            // Return the result as an API result object
            return Ok(result);
        }
    }
}
