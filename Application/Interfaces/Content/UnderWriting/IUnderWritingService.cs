using Application.Common;
using Application.Dtos.UnderWriting;
using Domain.Entities;

namespace Application.Interfaces.Content.UnderWriting;

public interface IUnderWritingService
{
    Task<ApiResult<UnderWritingForm>> CreateProductUnderWritingFormAsync(ProductUnderWritingDto model);
    Task<ApiResult<UnderWritingForm>> GetProductUnderWritingFormAsync(int productId);
    Task<ApiResult<ClaimsUnderWritingForm>> CreateClaimsUnderWritingFormAsync(ProductUnderWritingDto model);
    Task<ApiResult<ClaimsUnderWritingForm>> GetClaimsUnderWritingFormAsync(int productId);
    Task<ApiResult<FormSubmission>> SubmitProductUnderWritingFormAsync(FormSubmissionDto model);
    Task<ApiResult<ClaimsFormSubmission>> SubmitClaimsUnderWritingFormAsync(FormSubmissionDto model);
    Task<ApiResult<FormSubmission>> GetProductUnderWritingSubmissionAsync(string formId, string userId);
    Task<ApiResult<ClaimsFormSubmission>> GetClaimsUnderWritingSubmissionAsync(string formId, string userId);
    
}