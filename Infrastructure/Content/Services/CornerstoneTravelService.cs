using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;
namespace Infrastructure.Services
{
    public class CornerStoneTravelService(HttpClient httpClient) : ICornerStoneTravelService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ApiResult<TravelPollicyResponseDto>> GetPolicyAmountAsync(FormBody formBody)
        {
            // Validate the form before submitting
            var validationResult = ValidateForm(formBody);
            if (!validationResult.Success)
            {
                return ApiResult<TravelPollicyResponseDto>.Failed(validationResult.Message);
            }

            try
            {
                // Convert form fields to form-data
                var formDataContent = new MultipartFormDataContent();

                foreach (var field in formBody.GlobalFields)
                {
                    if (!string.IsNullOrWhiteSpace(field.DefaultValue))
                    {
                        formDataContent.Add(new StringContent(field.DefaultValue), field.FieldName);
                    }
                }

                if (formBody.Sections != null)
                {
                    foreach (var section in formBody.Sections)
                    {
                        foreach (var field in section.Fields)
                        {
                            if (!string.IsNullOrWhiteSpace(field.DefaultValue))
                            {
                                formDataContent.Add(new StringContent(field.DefaultValue), field.FieldName);
                            }
                        }
                    }
                }

                // POST the form data to the external API
                var response = await _httpClient.PostAsync("https://cornerstone.com.ng/devtest/rest/travel", formDataContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    // Deserialize the API response
                    var travelPollicyResponseDto = JsonConvert.DeserializeObject<TravelPollicyResponseDto>(responseData);

                    // Return the deserialized API response as the data
                    return ApiResult<TravelPollicyResponseDto>.Successful(travelPollicyResponseDto);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return ApiResult<TravelPollicyResponseDto>.NotFound("The requested resource was not found.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ApiResult<TravelPollicyResponseDto>.UnAuthorized("Unauthorized access.");
                }
                else
                {
                    return ApiResult<TravelPollicyResponseDto>.Failed("Failed to submit the form.");
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return ApiResult<TravelPollicyResponseDto>.InternalError($"An error occurred: {ex.Message}");
            }
        }

        // Validate each form field based on the provided rules
        private ApiResult<string> ValidateForm(FormBody formBody)
        {
            // Check global fields
            foreach (var field in formBody.GlobalFields)
            {
                if (field.IsRequired && string.IsNullOrWhiteSpace(field.DefaultValue))
                {
                    return ApiResult<string>.Failed($"The field {field.FieldName} is required.");
                }

                if (!string.IsNullOrWhiteSpace(field.RegexValidationPattern) && !string.IsNullOrWhiteSpace(field.DefaultValue))
                {
                    var regex = new Regex(field.RegexValidationPattern);
                    if (!regex.IsMatch(field.DefaultValue))
                    {
                        return ApiResult<string>.Failed($"The field {field.FieldName} is invalid.");
                    }
                }
            }

            // Validate sections (if present)
            if (formBody.Sections != null)
            {
                foreach (var section in formBody.Sections)
                {
                    foreach (var field in section.Fields)
                    {
                        if (field.IsRequired && string.IsNullOrWhiteSpace(field.DefaultValue))
                        {
                            return ApiResult<string>.Failed($"The field {field.FieldName} is required in section {section.SectionName}.");
                        }

                        if (!string.IsNullOrWhiteSpace(field.RegexValidationPattern) && !string.IsNullOrWhiteSpace(field.DefaultValue))
                        {
                            var regex = new Regex(field.RegexValidationPattern);
                            if (!regex.IsMatch(field.DefaultValue))
                            {
                                return ApiResult<string>.Failed($"The field {field.FieldName} is invalid in section {section.SectionName}.");
                            }
                        }
                    }
                }
            }

            // If all fields are valid
            return ApiResult<string>.Successful(null, "Form is valid.");
        }
    }
}