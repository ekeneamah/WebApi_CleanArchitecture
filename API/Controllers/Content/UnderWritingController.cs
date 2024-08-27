using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Common;
using Application.Dtos.UnderWriting;
using Application.Interfaces.Content.UnderWriting;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost("product-underwriting-form")]
    public async Task<ActionResult<ApiResult<UnderWritingForm>>> CreateProductUnderWritingForm([FromBody] ProductUnderWritingDto model)
    {
        var result = await _underWritingService.CreateProductUnderWritingFormAsync(model);
        return HandleOperationResult(result);
    }

    [HttpGet("product-underwriting-form")]
    public async Task<ActionResult<ApiResult<UnderWritingForm>>> GetProductUnderWritingForm([Required] int productId)
    {
        var result = await _underWritingService.GetProductUnderWritingFormAsync(productId);
        return HandleOperationResult(result);
    }

    [HttpPost("claims-underwriting-form")]
    public async Task<ActionResult<ApiResult<ClaimsUnderWritingForm>>> CreateClaimsUnderWritingForm([FromBody] ProductUnderWritingDto model)
    {
        var result = await _underWritingService.CreateClaimsUnderWritingFormAsync(model);
        return HandleOperationResult(result);
    }

    [HttpGet("claims-underwriting-form")]
    public async Task<ActionResult<ApiResult<ClaimsUnderWritingForm>>> GetClaimsUnderWritingForm([Required] int productId)
    {
        var result = await _underWritingService.GetClaimsUnderWritingFormAsync(productId);
        return HandleOperationResult(result);
    }

    [Authorize]
    [HttpPost("product-underwriting-submission")]
    public async Task<ActionResult<ApiResult<FormSubmission>>> SubmitProductUnderWritingForm([FromForm] FormSubmissionDto model)
    {
        var result = await _underWritingService.SubmitProductUnderWritingFormAsync(model);
        return HandleOperationResult(result);
    }

    [Authorize]
    [HttpPost("claims-underwriting-submission")]
    public async Task<ActionResult<ApiResult<ClaimsFormSubmission>>> SubmitClaimsUnderWritingForm([FromForm] FormSubmissionDto model)
    {
        var result = await _underWritingService.SubmitClaimsUnderWritingFormAsync(model);
        return HandleOperationResult(result);
    }

    [Authorize]
    [HttpGet("product-underwriting-submission")]
    public async Task<ActionResult<ApiResult<FormSubmission>>> GetProductUnderWritingSubmission([Required] string formId, [Required] string userId)
    {
        var result = await _underWritingService.GetProductUnderWritingSubmissionAsync(formId, userId);
        return HandleOperationResult(result);
    }

    [Authorize]
    [HttpGet("claims-underwriting-submission")]
    public async Task<ActionResult<ApiResult<ClaimsFormSubmission>>> GetClaimsUnderWritingSubmission([Required] string formId, [Required] string userId)
    {
        var result = await _underWritingService.GetClaimsUnderWritingSubmissionAsync(formId, userId);
        return HandleOperationResult(result);
    }
}
}