using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos.UnderWriting;
using Application.Interfaces.Content.UnderWriting;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services;

public class UnderWritingService : IUnderWritingService
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public UnderWritingService(AppDbContext dbContext, UserManager<AppUser> userManager)
    {
        _context = dbContext;
        _userManager = userManager;
    }

    public async Task<ApiResult<FormSubmission>> SubmitProductUnderWritingFormAsync(FormSubmissionDto model)
    {
        var underWritingForm = await _context.ProductUnderWritingForms
            .Include(underWritingForm => underWritingForm.Product).FirstOrDefaultAsync(x => x.Id == model.FormId);
        if (underWritingForm == null) return ApiResult<FormSubmission>.Failed("Form Not Found");


        var tokenUserId = HttpContextHelper.Current.User.FindFirst("UserId")?.Value;
        if (tokenUserId is null)
            return ApiResult<FormSubmission>.Failed("Invalid User");


        var user = await _userManager.FindByIdAsync(tokenUserId);
        if (user == null)
            return ApiResult<FormSubmission>.Failed("Invalid User");

        if (model.UserId != tokenUserId)
            return ApiResult<FormSubmission>.Failed("UserId Mismatch");

        var validationErrors = ValidateAnswer(model, underWritingForm.GlobalFields, underWritingForm.Sections ?? new List<FormSection>());
        if (validationErrors.Count > 0)
        {
            return ApiResult<FormSubmission>.Failed(string.Join(Environment.NewLine, validationErrors));
        }

        var data = await _context.ProductUnderWritingAnswers.FirstOrDefaultAsync(x =>
            x.UserId == model.UserId && x.FormId == underWritingForm.Id);
        var answers = ConvertDtoToFormAnswers(model.GlobalFieldAnswers);
        if (data is null)
        {
            data = new FormSubmission
            {
                FormId = model.FormId,
                UserId = model.UserId,
                GlobalFieldAnswers = answers,
                SubmissionDate = DateTime.Now,
                Status = model.Status,
                SectionSubmissions = ConvertSectionSubmissions(model.SectionSubmissions),
                Product = underWritingForm.Product

            };
            _context.ProductUnderWritingAnswers.Add(data);
        }
        else
        {
            data.GlobalFieldAnswers = answers;
            data.Status = model.Status;
            data.UserId = model.UserId;
            data.FormId = model.FormId;
            data.SectionSubmissions = ConvertSectionSubmissions(model.SectionSubmissions);
            data.Product = underWritingForm.Product;
            _context.Update(data);
        }
        

        if (underWritingForm.Product.RequireInspection)
        {
            data.InspectionStatus = InspectionStatus.NotStarted;
            //Todo: send email?
            //Todo: update inspection status
        }
        else if (model.Status == SubmissionStatus.Submitted)
        {
            //Todo: if cornerstone, create transaction and return transactionObject and payment
            //Todo: if Leadway, get quote, create transaction and return transactionObject and payment

            var transaction = new Transaction
            {
                Amount = 0,
                Reference = Guid.NewGuid().ToString(),
                DateTime = DateTime.Now,
                UserId = tokenUserId,
                ProductId = underWritingForm.Product.ProductId,
            };
            
            //Todo: save transaction and return transactionObject
            //Next steps: Initiate payment endpoint(specify payment gateway) - return payment link
            // Verify payment endpoint (specify payment gateway)
            // Automatic payment verification
            //upon verification, update transaction status, call provider to get policy - use automapper to map submission to provider dto?
            //Todo: Get vehicle makes and models endpoint.
            //Todo: create frontend data mapping to auto populate fields
            //e.g:
            // {
            //     "provider1": {
            //         "profileMappings": {
            //             "firstName": "clientInfo.othernames",
            //             "lastName": "clientInfo.surname",
            //             "email": "clientInfo.emailAddress",
            //             "phone": "clientInfo.mobileNo",
            //             "dob": "clientInfo.dob_mm_dd_yyyy"
            //         }
            //     },
            //     "provider2": {
            //         "profileMappings": {
            //             "firstName": "insured.firstName",
            //             "lastName": "insured.lastName",
            //             "email": "insured.email",
            //             "phone": "insured.phoneLine1",
            //             "dob": "insured.dateOfBirth"
            //         }
            //     }
            // }

        }
        await _context.SaveChangesAsync();

        return ApiResult<FormSubmission>.Successful(data);
    }

    public async Task<ApiResult<ClaimsFormSubmission>> SubmitClaimsUnderWritingFormAsync(FormSubmissionDto model)
    {
        var underWritingForm = await _context.ClaimsUnderWritingForms
            .Include(claimsUnderWritingForm => claimsUnderWritingForm.Product).FirstOrDefaultAsync(x => x.Id == model.FormId);
        if (underWritingForm == null) return ApiResult<ClaimsFormSubmission>.Failed("Form Not Found");


        var tokenUserId = HttpContextHelper.Current.User.FindFirst("UserId")?.Value;
        if (tokenUserId is null)
            return ApiResult<ClaimsFormSubmission>.Failed("Invalid User");


        var user = await _userManager.FindByIdAsync(tokenUserId);
        if (user == null)
            return ApiResult<ClaimsFormSubmission>.Failed("Invalid User");

        if (model.UserId != tokenUserId)
            return ApiResult<ClaimsFormSubmission>.Failed("UserId Mismatch");
        var validationErrors =  ValidateAnswer(model, underWritingForm.GlobalFields, underWritingForm.Sections ?? new List<FormSection>());
        if (validationErrors.Count > 0)
        {
            return ApiResult<ClaimsFormSubmission>.Failed(string.Join(Environment.NewLine, validationErrors));
        }

        var data = await _context.ClaimsUnderWritingAnswers.FirstOrDefaultAsync(x =>
            x.UserId == model.UserId && x.FormId == underWritingForm.Id);
        var answers = ConvertDtoToFormAnswers(model.GlobalFieldAnswers);
        if (data is null)
        {
            data = new ClaimsFormSubmission
            {
                FormId = model.FormId,
                UserId = model.UserId,
                GlobalFieldAnswers = answers,
                Status = model.Status,
                SectionSubmissions = ConvertSectionSubmissions(model.SectionSubmissions),
                Product = underWritingForm.Product


            };
            _context.ClaimsUnderWritingAnswers.Add(data);
        }
        else
        {
            data.GlobalFieldAnswers = answers;
            data.Status = model.Status;
            data.UserId = model.UserId;
            data.FormId = model.FormId;
            data.SectionSubmissions = ConvertSectionSubmissions(model.SectionSubmissions);
            data.Product = underWritingForm.Product;


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
                GlobalFields = model.GlobalFields,
                Sections = model.Sections,
                Product = product,
                Title = model.Title

            };
            _context.ProductUnderWritingForms.Add(data);
        }

        else
        {
            data.GlobalFields = model.GlobalFields;
            data.Sections = model.Sections;
            data.Title = model.Title;
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
                GlobalFields = model.GlobalFields,
                Sections = model.Sections,
                Product = product,
                Title = model.Title
                

            };
            _context.ClaimsUnderWritingForms.Add(data);
        }

        else
        {
            data.GlobalFields = model.GlobalFields;
            data.Sections = model.Sections;
            data.Title = model.Title;
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
        var tokenUserId = HttpContextHelper.Current.User.FindFirst("UserId")?.Value;
        if (tokenUserId is null)
            return ApiResult<FormSubmission>.Failed("Invalid User");


        var user = await _userManager.FindByIdAsync(tokenUserId);
        if (user == null)
            return ApiResult<FormSubmission>.Failed("Invalid User");

        if (userId != tokenUserId)
            return ApiResult<FormSubmission>.Failed("UserId Mismatch");


        var data = await _context.ProductUnderWritingAnswers.FirstOrDefaultAsync(x =>
            x.FormId == formId && x.UserId == userId);
        return ApiResult<FormSubmission>.Successful(data);
    }

    public async Task<ApiResult<ClaimsFormSubmission>> GetClaimsUnderWritingSubmissionAsync(string formId,
        string userId)
    {

        var tokenUserId = HttpContextHelper.Current.User.FindFirst("UserId")?.Value;
        if (tokenUserId is null)
            return ApiResult<ClaimsFormSubmission>.Failed("Invalid User");


        var user = await _userManager.FindByIdAsync(tokenUserId);
        if (user == null)
            return ApiResult<ClaimsFormSubmission>.Failed("Invalid User");

        if (userId != tokenUserId)
            return ApiResult<ClaimsFormSubmission>.Failed("UserId Mismatch");

        var data = await _context.ClaimsUnderWritingAnswers.FirstOrDefaultAsync(x =>
            x.FormId == formId && x.UserId == userId);
        return ApiResult<ClaimsFormSubmission>.Successful(data);
    }
    
    private List<string> ValidateForm(ProductUnderWritingDto form)
{
    var errors = new List<string>();

    // Validate Global Fields
    if (form.GlobalFields.Count == 0 && form.Sections.Count == 0)
    {
        errors.Add("At least one global field or section field must be defined in the form.");
    }

    // Validate GlobalFields
    if (form.GlobalFields.Count > 0)
    {
        var globalFieldNames = form.GlobalFields.Select(x => x.FieldName).ToList();

        if (HasDuplicates(globalFieldNames, true))
        {
            errors.Add("Form contains duplicate field names in global fields.");
        }

        foreach (var field in form.GlobalFields)
        {
            ValidateField(field, errors);
        }
    }

    // Validate Sections
    if (form.Sections.Count > 0)
    {
        foreach (var section in form.Sections)
        {
            ValidateSection(section, errors);
        }
    }

    return errors;
}

// Helper method to validate individual fields
private void ValidateField(FormField field, List<string> errors)
{
    if (string.IsNullOrEmpty(field.FieldName))
    {
        errors.Add("FieldName is required for a field.");
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
            errors.Add($"ValidationPattern '{field.RegexValidationPattern}' is not a valid regex for field {field.FieldName}.");
        }
    }

    // Validate file upload fields
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
                    errors.Add($"AllowedFileType '{fileType}' is not a valid file extension for field {field.FieldName}.");
                }
            }
        }
    }
}

// Helper method to validate FormSection
private void ValidateSection(FormSection section, List<string> errors)
{
    if (string.IsNullOrEmpty(section.SectionName))
    {
        errors.Add("SectionName is required for the section.");
    }

    var sectionFieldNames = section.Fields.Select(x => x.FieldName).ToList();

    if (HasDuplicates(sectionFieldNames, true))
    {
        errors.Add("Subsection contains duplicate field names.");
    }

    foreach (var field in section.Fields)
    {
        ValidateField(field, errors);
    }

    // Validate each subsection in the section
    foreach (var subSection in section.SubSections)
    {
        ValidateSubSection(subSection, errors);
    }
}

// Helper method to validate FormSubSection
private void ValidateSubSection(FormSubSection subSection, List<string> errors)
{
    if (string.IsNullOrEmpty(subSection.SubSectionName))
    {
        errors.Add("SubSectionName is required for the subsection.");
    }

    if (string.IsNullOrEmpty(subSection.SubSectionKey))
    {
        errors.Add("SubSectionKey is required for the subsection.");
    }

    var subSectionFieldNames = subSection.Fields.Select(x => x.FieldName).ToList();

    if (HasDuplicates(subSectionFieldNames, true))
    {
        errors.Add("Subsection contains duplicate field names.");
    }

    foreach (var field in subSection.Fields)
    {
        ValidateField(field, errors);
    }

    // Recursively validate nested subsections (SubSubSections)
    if (subSection.SubSubSections != null && subSection.SubSubSections.Count > 0)
    {
        foreach (var subSubSection in subSection.SubSubSections)
        {
            ValidateSubSection(subSubSection, errors);
        }
    }
}
    
    
    private List<string> ValidateAnswer(FormSubmissionDto submission, List<FormField> globalFields, List<FormSection> formSections)
{
    var errors = new List<string>();

    // Validate GlobalFieldAnswers
    foreach (var answer in submission.GlobalFieldAnswers)
    {
        var formField = globalFields.Find(f => f.FieldId.Equals(answer.FieldId, StringComparison.OrdinalIgnoreCase));
        if (formField == null)
        {
            errors.Add($"Field '{answer.FieldName}' is not a valid field in the form.");
            continue;
        }

        ValidateFieldAnswer(formField, answer, errors);
    }

    // Validate SectionSubmissions
    if (submission.SectionSubmissions != null)
    {
        foreach (var sectionSubmission in submission.SectionSubmissions)
        {
            var formSection = formSections.Find(s => s.SectionId.Equals(sectionSubmission.SectionId, StringComparison.OrdinalIgnoreCase));
            if (formSection == null)
            {
                errors.Add($"Section '{sectionSubmission.SectionKey}' is not a valid section in the form.");
                continue;
            }

            foreach (var entrySubmission in sectionSubmission.EntrySubmissions)
            {
                foreach (var fieldResponse in entrySubmission.FieldResponses)
                {
                    var formField = formSection.Fields.Find(f => f.FieldId.Equals(fieldResponse.FieldId, StringComparison.CurrentCultureIgnoreCase));
                    if (formField == null)
                    {
                        errors.Add($"Field '{fieldResponse.FieldName}' in section '{sectionSubmission.SectionKey}' is not a valid field.");
                        continue;
                    }

                    ValidateFieldAnswer(formField, fieldResponse, errors);
                }
            }
        }
    }

    // Check for required fields that are missing in GlobalFieldAnswers
    foreach (var field in globalFields)
    {
        if (submission.Status == SubmissionStatus.Submitted && field.IsRequired)
        {
            var answer = submission.GlobalFieldAnswers.Find(a => a.FieldId.Equals(field.FieldId, StringComparison.OrdinalIgnoreCase));

            if (answer == null ||
                !answer.Values.Any() || answer.Values.TrueForAll(x => x == null) /* &&
                (answer.Files == null || !answer.Files.Any()) */)
            {
                errors.Add($"Field '{field.FieldName}' is required and cannot be empty.");
            }
        }
    }

    // Check for required fields that are missing in SectionSubmissions
    foreach (var section in formSections)
    {
        foreach (var field in section.Fields)
        {
            if (submission.Status == SubmissionStatus.Submitted && field.IsRequired)
            {
                bool fieldFound = false;
                if (submission.SectionSubmissions != null)
                {
                    foreach (var sectionSubmission in submission.SectionSubmissions)
                    {
                        var entrySubmission = sectionSubmission.EntrySubmissions.Find(e => e.FieldResponses.Exists(f => f.FieldId.Equals(field.FieldId, StringComparison.OrdinalIgnoreCase)));
                        if (entrySubmission != null)
                        {
                            fieldFound = true;
                            break;
                        }
                    }
                }

                if (!fieldFound)
                {
                    errors.Add($"Field '{field.FieldName}' in section '{section.SectionName}' is required and cannot be empty.");
                }
            }
        }
    }

    return errors;
}

// Helper method to validate field answers
private void ValidateFieldAnswer(FormField formField, FormAnswerDto answer, List<string> errors)
{
    if (formField.IsRequired && answer.Values.Any())
    {
        if (!answer.Values.Any() /* && (answer.Files == null || !answer.Files.Any()) */)
        {
            errors.Add($"Field '{formField.FieldName}' is required and cannot be empty.");
        }
    }

    if (!formField.AllowsMultiple && answer.Values.Count > 1)
    {
        errors.Add($"Field '{formField.FieldName}' does not allow multiple answers.");
    }

    // if (!formField.AllowsMultiple /* && answer.Files != null && answer.Files.Count > 1 */)
    // {
    //     errors.Add($"Field '{formField.FieldName}' does not allow multiple file uploads.");
    // }

    if (formField.InputType == InputType.Select)
    {
        foreach (var value in answer.Values)
        {
            if (formField.Options != null && !formField.Options.Any(o => o.Key == value))
            {
                errors.Add($"Value '{value}' is not a valid option for field '{formField.FieldName}'.");
            }
        }
    }

    // if (formField.InputType == InputType.Upload && answer.Files != null && answer.Files.Any())
    // {
    //     foreach (var file in answer.Files)
    //     {
    //         if (formField.MaxFileSize.HasValue && file.Length > formField.MaxFileSize.Value)
    //         {
    //             errors.Add($"File '{file.FileName}' exceeds the maximum allowed size for field '{formField.FieldName}'.");
    //         }
    //
    //         var fileExtension = Path.GetExtension(file.FileName);
    //         if (formField.AllowedFileTypes != null && !formField.AllowedFileTypes.Contains(fileExtension))
    //         {
    //             errors.Add($"File '{file.FileName}' is not of an allowed type for field '{formField.FieldName}'.");
    //         }
    //     }
    // }

    if (!string.IsNullOrEmpty(formField.RegexValidationPattern))
    {
        var regex = new Regex(formField.RegexValidationPattern);
        foreach (var value in answer.Values)
        {
            if (!regex.IsMatch(value))
            {
                errors.Add($"Value '{value}' does not match the validation pattern for field '{formField.FieldName}'.");
            }
        }
    }
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

    private List<SectionSubmission>? ConvertSectionSubmissions(List<SectionSubmissionDto>? sectionSubmissionsDto)
    {
        if (sectionSubmissionsDto == null)
        {
            return null;
        }

        return sectionSubmissionsDto.Select(dto => new SectionSubmission
        {
            SectionId = dto.SectionId,
            SectionKey = dto.SectionKey,
            EntrySubmissions = dto.EntrySubmissions.Select(entryDto => new SectionEntrySubmission
            {
                FieldResponses = ConvertDtoToFormAnswers(entryDto.FieldResponses),
                SubSectionSubmissions = entryDto.SubSectionSubmissions.Select(subSectionDto => new SubSectionSubmission
                {
                    SubSectionId = subSectionDto.SubSectionId,
                    FieldResponses = ConvertDtoToFormAnswers(subSectionDto.FieldResponses)
                }).ToList()
            }).ToList()
        }).ToList();
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

        foreach (var answer in submission.GlobalFieldAnswers)
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
    
