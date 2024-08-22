﻿using System.Net.Http.Headers;
using System.Text;
using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Content
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MotorClaimsController : BaseController
    {
        private readonly IMotorClaimRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly HttpClient _httpClient;

        public MotorClaimsController(IMotorClaimRepository repository,
            HttpClient httpClient,
            UserManager<AppUser> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager;
            _httpClient = httpClient;

        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<MotorClaim>>>> GetAll()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");

            var claims = await _repository.GetAllMotorClaims(user.Id);
            return HandleOperationResult(claims);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<MotorClaim>>> GetById(int id)
        {
            var claim = await _repository.GetMotorClaimById(id);
            return HandleOperationResult(claim);

        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<MotorClaim>>> Create(MotorClaim motorClaim)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            motorClaim.UserId = user.Id;
           // motorClaim.Name = user.FirstName+" "+user.LastName;

            HttpResponseMessage claimsRes = await SendClaimAsync(motorClaim);
            if (claimsRes.IsSuccessStatusCode)
            {

                var createdClaim = await _repository.CreateMotorClaim(motorClaim);
                return HandleOperationResult(createdClaim);
            }
            else
            {
                return BadRequest("Claims could not be created");
            }
        }

        [NonAction]
        public async Task<HttpResponseMessage> SendClaimAsync(MotorClaim claimRequest)
        {
            // API endpoint
            string endpoint = "https://app.cornerstone.com.ng/claimapi/api/ProcessClaim/NewClaim";

            // Serialize the claimRequest object to JSON
            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(claimRequest);

            // Create HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // UpdateUser basic authentication header
            var authValue = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes("c31a6a20-7eda-41da-a7df-d14b501c237c:DECHANNEL")));
            _httpClient.DefaultRequestHeaders.Authorization = authValue;

            // Send the request and get the response
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response;
        }

    

    [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MotorClaim motorClaim)
        {
            if (id != motorClaim.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateMotorClaim(motorClaim);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingClaim = await _repository.GetMotorClaimById(id);
            if (existingClaim == null)
            {
                return NotFound();
            }
            await _repository.DeleteMotorClaim(id);
            return NoContent();
        }
    }
}
