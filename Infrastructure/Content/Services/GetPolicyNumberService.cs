using Application.Dtos;
using Domain.Entities;
using Infrastructure.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<PolicyGenReturnedData> PostPolicyAndTransform(string endpointUrl, GetPolicyNumberDTO policyDto)
        {
            // Serialize the PolicyDTO to JSON
            var jsonContent = JsonSerializer.Serialize(policyDto);

            try
            {
                // Create the HTTP POST request
                var response = await _httpClient.PostAsync(endpointUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the response JSON into the target model
                    var modelForDatabase = JsonSerializer.Deserialize<PolicyGenReturnedData>(responseContent);

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
