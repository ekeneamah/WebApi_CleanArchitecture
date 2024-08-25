using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos.UnderWriting;
using Application.Interfaces.Content.UnderWriting;
using Domain.Entities;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services;

public class UnderWritingService : IUnderWritingService
{
    private readonly AppDbContext _context;

    public UnderWritingService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<ApiResult<UnderWritingForm>> CreateProductUnderWritingFormAsync(ProductUnderWritingDto model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        if (product == null) return  ApiResult<UnderWritingForm>.Failed("Product Not Found");

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
        if (product == null) return  ApiResult<UnderWritingForm>.Failed("Product Not Found");
     
        var data = await _context.ProductUnderWritingForms.FirstOrDefaultAsync(x => x.Product == product);
        return ApiResult<UnderWritingForm>.Successful(data);
    }
    
    
    public async Task<ApiResult<ClaimsUnderWritingForm>> CreateClaimsUnderWritingFormAsync(ProductUnderWritingDto model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        if (product == null) return  ApiResult<ClaimsUnderWritingForm>.Failed("Product Not Found");

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
        if (product == null) return  ApiResult<ClaimsUnderWritingForm>.Failed("Product Not Found");
     
        var data = await _context.ClaimsUnderWritingForms.FirstOrDefaultAsync(x => x.Product == product);
        return ApiResult<ClaimsUnderWritingForm>.Successful(data);
    }
    
    
    private List<string> ValidateForm(ProductUnderWritingDto form)
    {
        var errors = new List<string>();

      
        if ( form.Form.Count == 0)
        {
            errors.Add("At least one field must be defined in the form.");
        }
        else
        {
            foreach (var field in form.Form)
            {
               

                // Validate regex pattern if provided
                if (!string.IsNullOrEmpty(field.RegexValidationPattern))
                {
                    try
                    {
                        _ = new Regex(field.RegexValidationPattern);
                    }
                    catch (ArgumentException)
                    {
                        errors.Add($"ValidationPattern '{field.RegexValidationPattern}' is not a valid regex for field {field.FieldName}.");
                    }
                }

                if (field.InputType != InputType.Upload) continue;
                if (field.MaxFileSize.HasValue && field.MaxFileSize.Value <= 0)
                {
                    errors.Add($"MaxFileSize must be greater than 0 for field {field.FieldName}.");
                }

                // Validate AllowedFileTypes
                if (field.AllowedFileTypes != null && field.AllowedFileTypes.Count > 0)
                {
                    foreach (var fileType in field.AllowedFileTypes)
                    {
                        if (string.IsNullOrEmpty(fileType) || !fileType.StartsWith('.'))
                        {
                            errors.Add($"AllowedFileType '{fileType}' is not a valid file extension for field {field.FieldName}.");
                        }
                    }
                }
            }
        }

        return errors;
    }
    
}