using Application.Common;

namespace API.Controllers.Content
{
    using Application.Dtos;
    using Application.Dtos.Account;
    using Application.Interfaces.Content.Claim;
    using Infrastructure.Identity.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

        
        [Route("api/claims")]
        [ApiController]
        [Authorize]
        public class ClaimController : BaseController
        {
            private readonly IClaim _claimService;
            private readonly UserManager<AppUser> _userManager;
        private readonly HttpClient _httpClient;



        public ClaimController(IClaim claimService, UserManager<AppUser> userManager,HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
           
                _claimService = claimService;
            _userManager = userManager;
            }

            [HttpGet]
            public async Task<ActionResult<ApiResult<List<ClaimDetailDto>>>> GetAllClaims()
            {
                var claims = await _claimService.GetAll();
                return HandleOperationResult(claims);
            }
        #region Get All my claims
        [HttpGet("me")]
        public async Task<ActionResult<ApiResult<List<ClaimDetailDto>>>> GetAllMyClaims()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return HandleOperationResult( ApiResult<List<ClaimDetailDto>>.Failed("Invalid User"));

            var claims = await _claimService.GetAllMyClaims(user.Id);
            return HandleOperationResult(claims);
        }
        #endregion
        #region get claim by id
        [HttpGet("{claimId}")]
            public async Task<ActionResult<ApiResult<ClaimDetailDto>>> GetClaimById(string claimId)
            {
                var claim = await _claimService.GetById(claimId);
                return HandleOperationResult(claim);

            }
#endregion
       
        #region create claim 
        [HttpPost]
        
        public async Task<IActionResult> PostClaims([FromBody] ClaimsDto claimsDto)
        {
            // Your authorization logic can go here
            string token = await AuthenticateAndGetToken();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Failed to obtain authentication token.");
            }
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
                if (user == null)
                    return BadRequest("Invalid User");

                
                // Serialize the claimsDto object to JSON
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(claimsDto);

                // Create the HTTP request message
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://testcipapiservices.gibsonline.com/api/claims"),
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                };

                // UpdateUser Authorization header
                request.Headers.Add("Authorization", $"Bearer {token}");

                // Send the request and get the response
                var response = await _httpClient.SendAsync(request);


                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    claimsDto.UserId = user.Id;
                    var c = (await _claimService.AddClaims(claimsDto)).Data;
                    var r = await response.Content.ReadAsStringAsync();
                    NotificationDto tokenObject = System.Text.Json.JsonSerializer.Deserialize<NotificationDto>(r);
                    tokenObject.ClaimsId = c.ClaimId;
                    var addedClaim = await _claimService.AddNotification(tokenObject);
                    return Ok("Claim submitted successfully "+ addedClaim);
                }
                else
                {
                    // Log the error message
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ClaimsErrorDto tokenObject = System.Text.Json.JsonSerializer.Deserialize<ClaimsErrorDto>(errorMessage);
                    Console.WriteLine($"Error occurred while submitting claim: {errorMessage}");

                    // Return an error response
                    return BadRequest(tokenObject);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error processing claim: {ex.Message}");

                // Return an error response
                return StatusCode(500, "An error occurred while processing the claim");
            }
        }
        #endregion

        #region get notifcation 
        [HttpGet("notifications/{id}")]
        public async Task<IActionResult> GetNotification(int notificationNo)
        {
            // Your authorization logic can go here
            string token = await AuthenticateAndGetToken();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Failed to obtain authentication token.");
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://testcipapiservices.gibsonline.com/api/"),
                
            };

            // UpdateUser Authorization header
            request.Headers.Add("Authorization", $"Bearer {token}");

            // Send the request and get the response
            var response = await _httpClient.GetAsync($"claims/{notificationNo}");

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a NotificationDTO object
                var notificationDTO = JsonConvert.DeserializeObject<NotificationDto>(content);
               var result = await _claimService.AddNotification(notificationDTO);

                // Return the NotificationDTO object
                if(result == null)
                    return BadRequest("Could not save notification");
                return Ok(result);
            }
            else
            {
                // Return the status code and reason phrase
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        #endregion

        #region authenticate
        private static async Task<string> AuthenticateAndGetToken()
        {
            var authApiUrl = "https://testcipapiservices.gibsonline.com/api/auth";

            var username = "Transcape";
            var password = "Transcape@321#";

            using var client = new HttpClient();
            var authData = new
            {
                username,
                password
            };

            var jsonAuthData = System.Text.Json.JsonSerializer.Serialize(authData);
            var content = new StringContent(jsonAuthData, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(authApiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                RespondData tokenObject = System.Text.Json.JsonSerializer.Deserialize<RespondData>(responseData);
                return tokenObject.AccessToken;
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region update claim

        [HttpPut("{claimId}")]
            public async Task<ActionResult<ApiResult<ClaimsDto>>> UpdateClaim(string claimId,[FromBody]  ClaimsDto model)
            {
                if (claimId != model.ClaimId.ToString())
                {
                    return BadRequest(); // 400 Bad Request if IDs don't match
                }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return HandleOperationResult( ApiResult<ClaimsDto>.Failed("Invalid User"));
            }
            model.UserId = user?.Id;
            var updatedClaim = await _claimService.UpdateClaims(model);

                return HandleOperationResult(updatedClaim);

            }
        }
    #endregion
    public class RespondData
    {
        public string AccessToken { get; set; }
    }
}


