using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos.UnderWriting;
using Application.Interfaces.Content.UnderWriting;
using Domain.Entities;
using Infrastructure.Content.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services;

public class UnderWritingService : IUnderWritingService
{
    private readonly AppDbContext _context;

    public UnderWritingService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<ApiResult<FormSubmission>> SubmitProductUnderWritingFormAsync(FormSubmissionDto model)
    {
        var underWritingForm = await _context.ProductUnderWritingForms.FirstOrDefaultAsync(x => x.Id == model.FormId);
        if (underWritingForm == null) return ApiResult<FormSubmission>.Failed("Form Not Found");

        //Todo: compare userId with Logged in User

        var validationErrors = ValidateAnswer(model, underWritingForm.Form);
        if (validationErrors.Count > 0)
        {
            return ApiResult<FormSubmission>.Failed(string.Join(Environment.NewLine, validationErrors));
        }

        var data = await _context.ProductUnderWritingAnswers.FirstOrDefaultAsync(x =>
            x.UserId == model.UserId && x.FormId == underWritingForm.Id);
        var answers = ConvertDtoToFormAnswers(model.Answers);
        if (data is null)
        {
            data = new FormSubmission
            {
                FormId = model.FormId,
                UserId = model.UserId,
                Answers = answers,
                Status = model.Status

            };
            _context.ProductUnderWritingAnswers.Add(data);
        }
        else
        {
            data.Answers = answers;
            data.Status = model.Status;
            data.UserId = model.UserId;
            data.FormId = model.FormId;

            _context.Update(data);
        }

        await _context.SaveChangesAsync();
        return ApiResult<FormSubmission>.Successful(data);
    }

    public async Task<ApiResult<ClaimsFormSubmission>> SubmitClaimsUnderWritingFormAsync(FormSubmissionDto model)
    {
        var underWritingForm = await _context.ClaimsUnderWritingForms.FirstOrDefaultAsync(x => x.Id == model.FormId);
        if (underWritingForm == null) return ApiResult<ClaimsFormSubmission>.Failed("Form Not Found");

        //Todo: compare userId with Logged in User

        var validationErrors = ValidateAnswer(model, underWritingForm.Form);
        if (validationErrors.Count > 0)
        {
            return ApiResult<ClaimsFormSubmission>.Failed(string.Join(Environment.NewLine, validationErrors));
        }

        var data = await _context.ClaimsUnderWritingAnswers.FirstOrDefaultAsync(x =>
            x.UserId == model.UserId && x.FormId == underWritingForm.Id);
        var answers = ConvertDtoToFormAnswers(model.Answers);
        if (data is null)
        {
            data = new ClaimsFormSubmission
            {
                FormId = model.FormId,
                UserId = model.UserId,
                Answers = answers,
                Status = model.Status

            };
            _context.ClaimsUnderWritingAnswers.Add(data);
        }
        else
        {
            data.Answers = answers;
            data.Status = model.Status;
            data.UserId = model.UserId;
            data.FormId = model.FormId;

            _context.Update(data);
        }

        await _context.SaveChangesAsync();
        return ApiResult<ClaimsFormSubmission>.Successful(data);
    }

    public async Task<ApiResult<UnderWritingForm>> CreateProductUnderWritingFormAsync(ProductUnderWritingDto model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        if (product == null) return ApiResult<UnderWritingForm>.Failed("Product Not Found");

        var validationErrors = ValidateForm(model);
        if (validationErrors.Count > 0)
        {
            return ApiResult<UnderWritingForm>.Failed(string.Join(Environment.NewLine, validationErrors));
        }

        var data = await _context.ProductUnderWritingForms.FirstOrDefaultAsync(x => x.Product == product);
        if (data is null)
        {
            data = new UnderWritingForm
            {
                Form = model.Form,
                Product = product

            };
            _context.ProductUnderWritingForms.Add(data);
        }

        else
        {
            data.Form = model.Form;
            _context.Update(data);
        }

        await _context.SaveChangesAsync();
        return ApiResult<UnderWritingForm>.Successful(data);
    }

    public async Task<ApiResult<UnderWritingForm>> GetProductUnderWritingFormAsync(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        if (product == null) return ApiResult<UnderWritingForm>.Failed("Product Not Found");

        var data = await _context.ProductUnderWritingForms.FirstOrDefaultAsync(x => x.Product == product);
        return ApiResult<UnderWritingForm>.Successful(data);
    }


    public async Task<ApiResult<ClaimsUnderWritingForm>> CreateClaimsUnderWritingFormAsync(ProductUnderWritingDto model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        if (product == null) return ApiResult<ClaimsUnderWritingForm>.Failed("Product Not Found");

        var validationErrors = ValidateForm(model);
        if (validationErrors.Count > 0)
        {
            return ApiResult<ClaimsUnderWritingForm>.Failed(string.Join(Environment.NewLine, validationErrors));
        }

        var data = await _context.ClaimsUnderWritingForms.FirstOrDefaultAsync(x => x.Product == product);
        if (data is null)
        {
            data = new ClaimsUnderWritingForm
            {
                Form = model.Form,
                Product = product

            };
            _context.ClaimsUnderWritingForms.Add(data);
        }

        else
        {
            data.Form = model.Form;
            _context.Update(data);
        }

        await _context.SaveChangesAsync();
        return ApiResult<ClaimsUnderWritingForm>.Successful(data);
    }

    public async Task<ApiResult<ClaimsUnderWritingForm>> GetClaimsUnderWritingFormAsync(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        if (product == null) return ApiResult<ClaimsUnderWritingForm>.Failed("Product Not Found");

        var data = await _context.ClaimsUnderWritingForms.FirstOrDefaultAsync(x => x.Product == product);
        return ApiResult<ClaimsUnderWritingForm>.Successful(data);
    }

    
    public async Task<ApiResult<FormSubmission>> GetProductUnderWritingSubmissionAsync(string formId, string userId)
    {

        var data = await _context.ProductUnderWritingAnswers.FirstOrDefaultAsync(x => x.FormId == formId && x.UserId == userId);
        return ApiResult<FormSubmission>.Successful(data);
    }
    
 public async Task<ApiResult<ClaimsFormSubmission>> GetClaimsUnderWritingSubmissionAsync(string formId, string userId)
    {

        var data = await _context.ClaimsUnderWritingAnswers.FirstOrDefaultAsync(x => x.FormId == formId && x.UserId == userId);
        return ApiResult<ClaimsFormSubmission>.Successful(data);
    }

    private List<string> ValidateForm(ProductUnderWritingDto form)
    {
        var errors = new List<string>();


        if (form.Form.Count == 0)
        {
            errors.Add("At least one field must be defined in the form.");
        }   
        
        else
        {
            var formFields = form.Form.Select(x => x.FieldName).ToList();

            if (HasDuplicates(formFields, true))
            {
                errors.Add("Form contains duplicate field names.");
                return errors;
            
            }
            foreach (var field in form.Form)
            {
                if (string.IsNullOrEmpty(field.FieldName))
                {
                    errors.Add($"FieldName is required for a field.");
                }

                // Validate regex pattern if provided
                if (!string.IsNullOrEmpty(field.RegexValidationPattern))
                {
                    try
                    {
                        _ = new Regex(field.RegexValidationPattern);
                    }
                    catch (ArgumentException)
                    {
                        errors.Add(
                            $"ValidationPattern '{field.RegexValidationPattern}' is not a valid regex for field {field.FieldName}.");
                    }
                }

                if (field.InputType == InputType.Upload)
                {
                    if (field.MaxFileSize.HasValue && field.MaxFileSize.Value <= 0)
                    {
                        errors.Add($"MaxFileSize must be greater than 0 for field {field.FieldName}.");
                    }

                    if (field.AllowedFileTypes != null && field.AllowedFileTypes.Count > 0)
                    {
                        foreach (var fileType in field.AllowedFileTypes)
                        {
                            if (string.IsNullOrEmpty(fileType) || !fileType.StartsWith("."))
                            {
                                errors.Add(
                                    $"AllowedFileType '{fileType}' is not a valid file extension for field {field.FieldName}.");
                            }
                        }
                    }
                }
            }
        }

        return errors;
    }

    
     private List<string> ValidateAnswer(FormSubmissionDto submission, List<FormField> form)
    {
        var errors = new List<string>();

        foreach (var answer in submission.Answers)
        {
            var formField = form.Find(f => f.FieldName == answer.FieldName);
            if (formField == null)
            {
                errors.Add($"Field '{answer.FieldName}' is not a valid field in the form.");
                continue;
            }
            if (formField.IsRequired && answer.Values.Any())
            {
                if (!answer.Values.Any() && (answer.Files == null || !answer.Files.Any()))
                {
                    errors.Add($"Field '{formField.FieldName}' is required and cannot be empty.");
                }
            }

            if (!formField.AllowsMultiple && answer.Values.Count > 1)
            {
                errors.Add($"Field '{formField.DisplayName}' does not allow multiple answers.");
            }

            if (!formField.AllowsMultiple && answer.Files != null && answer.Files.Count > 1)
            {
                errors.Add($"Field '{formField.DisplayName}' does not allow multiple file uploads.");
            }

            if (formField.InputType == InputType.Upload && answer.Files != null && answer.Files.Any())
            {
                foreach (var file in answer.Files)
                {
                    if (formField.MaxFileSize.HasValue && file.Length > formField.MaxFileSize.Value)
                    {
                        errors.Add($"File '{file.FileName}' exceeds the maximum allowed size for field '{formField.DisplayName}'.");
                    }
            
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (formField.AllowedFileTypes != null && !formField.AllowedFileTypes.Contains(fileExtension))
                    {
                        errors.Add($"File '{file.FileName}' is not of an allowed type for field '{formField.DisplayName}'.");
                    }
                }
            }

            if (!string.IsNullOrEmpty(formField.RegexValidationPattern))
            {
                var regex = new Regex(formField.RegexValidationPattern);
                foreach (var value in answer.Values)
                {
                    if (!regex.IsMatch(value))
                    {
                        errors.Add($"Value '{value}' does not match the validation pattern for field '{formField.DisplayName}'.");
                    }
                }
            }
            
        }

        // Check for required fields that are missing in the answers
        foreach (var field in form)
        {
            if (submission.Status == SubmissionStatus.Submitted && field.IsRequired)
            {
                var answer = submission.Answers.Find(a => a.FieldName == field.FieldName);
    
                if (answer == null || 
                      !answer.Values.Any() || answer.Values.TrueForAll(x => x == null) && 
                    (answer.Files == null || !answer.Files.Any()))
                {
                    errors.Add($"Field '{field.FieldName}' is required and cannot be empty.");
                }
            }
        }

        return errors;
    }


    private List<FormAnswer> ConvertDtoToFormAnswers(List<FormAnswerDto> dtos)
    {
        var formAnswers = new List<FormAnswer>();

        foreach (var dto in dtos)
        {
            var formAnswer = new FormAnswer
            {
                FieldName = dto.FieldName,
                FieldId = dto.FieldId,
                Values = dto.Values,
                // FileUrls = dto.Files?.Select(file => ConvertToBase64(file)).ToList()
            };

            //Todo: upload file to blob and get url
            formAnswers.Add(formAnswer);
        }

        return formAnswers;
    }

    private string ConvertToBase64(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }
    
    private static bool HasDuplicates(List<string> strings, bool ignoreCase)
    {
        var set = new HashSet<string>(ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);

        foreach (var str in strings)
        {
            if (!set.Add(str))
            {
                return true;
            }
        }
        return false;
    }
     public Dictionary<string, object> ConvertToDictionary(FormSubmission submission, List<FormField> form)
    {
        var result = new Dictionary<string, object>();

        foreach (var answer in submission.Answers)
        {
            var formField = form.Find(f => f.FieldName == answer.FieldName);
            if (formField != null)
            {
                if (formField.InputType == InputType.Upload && answer.FileUrls != null && answer.FileUrls.Any())
                {
                    if (formField.AllowsMultiple)
                    {
                        // var fileContents = answer.Files.Select(file => new
                        // {
                        //     FileName = file.FileName,
                        //     ContentType = file.ContentType,
                        //     Content = ConvertFileToBase64(file)
                        // }).ToArray();
                        // result[answer.FieldName] = fileContents;
                    }
                    else
                    {
                        var file = answer.FileUrls[0];
                        // result[answer.FieldName] = new
                        // {
                        //     FileName = file.FileName,
                        //     ContentType = file.ContentType,
                        //     Content = ConvertFileToBase64(file)
                        // };
                    }
                }
                else if (formField.AllowsMultiple)
                {
                    result[answer.FieldName] = answer.Values.ToArray();
                }
                else
                {
                    result[answer.FieldName] = answer.Values.FirstOrDefault()!;
                }
            }
        }

        return result;
    }
    
}
    
