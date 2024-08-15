using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Content.Policy;
using Domain.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Text;
using System.Text.Json;
using TransactionDto = Application.Dtos.TransactionDto;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicy _policyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ITransaction _transactionService;
        public PolicyController(IPolicy policyService,UserManager<AppUser> userManager, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _policyService = policyService;
            _userManager = userManager;
        }
        #region create policy
        [HttpPost]
        public ActionResult<int> Create(TransactionDto policyDTO)
        {
            return Ok(_policyService.AddPolicy(policyDTO));
        }
        #endregion

        #region get my policies
        [HttpGet("GetMyPolicies")]
        public async Task<ActionResult> GetMyPolicies()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            return Ok(await _policyService.GetAllPolicyByUserId(user.Id));
        }
        #endregion
        #region get policy by customer name
        [HttpGet("GetByUserName")]
        public async Task<ActionResult> GetByUserName()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            return Ok(await _policyService.GetByUserName(user.Id));
        }
        #endregion
        #region get policy number from insurance
        /// <summary>
        /// Get Customer policy number from Cornerstone
        /// If the policy no is available then it will call the get certifcate endpoint
        /// </summary>
        /// <param name="generatePolicyDTO"></param>
        /// <returns></returns>
        [HttpPost("GenPolicyNo")]
        public async Task<ActionResult<string>> GenPolicyNo(GeneratePolicyDto generatePolicyDTO)
        {
            string token = await AuthenticateAndGetToken();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Failed to obtain authentication token.");
            }
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            generatePolicyDTO.Userid = user.Id;
            generatePolicyDTO.Token = token;
            return await _policyService.GeneratePolicyNumber(generatePolicyDTO);
            
        }
        #endregion
        #region Update policy
        [HttpPut("Update")]
        public async Task<ActionResult> UpdatePolicies(TransactionDto policy)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            policy.UserId = user.Id;
            return Ok(_policyService.UpdatePolicy(policy));
        }
        #endregion
        #region fetch certifcate
        [HttpGet("FetchCertificate")]
        public async Task<IActionResult> FetchCertificate(string policyNo, string email)
        {
            // Authenticate to obtain token
            string token = await AuthenticateAndGetToken();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Failed to obtain authentication token.");
            }
            string apiUrl = $"https://testcipapiservices.gibsonline.com/api/utilities/fetch/certificate?policyNo={policyNo}&email={email}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var fileName = $"certificate_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "policycert", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await contentStream.CopyToAsync(fileStream);
                }

                // Save the file path to the database
                // You need to implement your database logic here

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/pdf", fileName);
            }
            else
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch certificate. Status code: {response.StatusCode}");
            }
        }

        #endregion

        #region initialised

        private static async Task<string> AuthenticateAndGetToken()
        {
            var authApiUrl = "https://testcipapiservices.gibsonline.com/api/auth";

            var username = "Transcape";
            var password = "Transcape@321#";

            using (var client = new HttpClient())
            {
                var authData = new
                {
                    username,
                    password
                };

                var jsonAuthData = JsonSerializer.Serialize(authData);
                var content = new StringContent(jsonAuthData, Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(authApiUrl, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var tokenObject = JsonSerializer.Deserialize<AuthResponse>(responseData);
                        return tokenObject.accessToken;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private class AuthResponse
        {
            public string accessToken { get; set; }
        }
        #endregion
    }
}
