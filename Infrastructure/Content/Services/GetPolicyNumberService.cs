using Application.Dtos;
using Domain.Entities;
using Infrastructure.Identity.Services;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Content.Services
{
    public class GetPolicyNumberService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthResponseService _authResponseService;
        public GetPolicyNumberService(HttpClient httpClient, AuthResponseService authResponseService)
        {
            _httpClient = httpClient;
            _authResponseService = authResponseService;
        }

        public async Task<PolicyGenReturnedDataCornerstoneDto> PostPolicyAndTransform(string endpointUrl, GetPolicyNumberDto policyDto)
        {
            // Serialize the TransactionDTO to JSON
            var jsonContent = JsonSerializer.Serialize(policyDto);
            

            try
            {
                // Create the HTTP POST request
                var response = await _httpClient.PostAsync(endpointUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {policyDto.Token}");
                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the response JSON into the target model
                    var modelForDatabase = JsonSerializer.Deserialize<PolicyGenReturnedDataCornerstoneDto>(responseContent);
                  
                    return modelForDatabase;
                }
                else
                {
                    // Handle unsuccessful API response (e.g., log or throw an exception)
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., log or rethrow)
                throw new Exception("Failed to post policy and transform response.", ex);
            }
        }
    }
}
