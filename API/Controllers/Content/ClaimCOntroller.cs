namespace API.Controllers.Content
{
    using Application.Dtos;
    using Application.Interfaces.Content.Claim;
    using Domain.Models;
    using Infrastructure.Identity.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

        
        [Route("api/[controller]")]
        [ApiController]
        [Authorize]
        public class ClaimController : ControllerBase
        {
            private readonly IClaim _claimService;
            private readonly UserManager<AppUser> _userManager; 


            public ClaimController(IClaim claimService, UserManager<AppUser> userManager)
            {
                _claimService = claimService;
            _userManager = userManager;
            }

            [HttpGet]
            public async Task<ActionResult<List<ClaimsDto>>> GetAllClaims()
            {
                var claims = await _claimService.GetAll();
                return Ok(claims);
            }
        #region get claim by id
        [HttpGet("{claimId}")]
            public async Task<ActionResult<ClaimsDto>> GetClaimById(string claimId)
            {
                var claim = await _claimService.GetById(claimId);
                if (claim == null)
                {
                    return NotFound(); // 404 Not Found if claim not found
                }
                return Ok(claim);
            }
#endregion
        #region Create claim
        [HttpPost]
            public async Task<ActionResult<ClaimsDto>> AddClaim(ClaimsDto model)
            {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");

            model.UserId = user.Id;
            var addedClaim = await _claimService.AddClaims(model);
                return CreatedAtAction(nameof(GetClaimById), new { claimId = addedClaim.PolicyId }, addedClaim);
            }
        #endregion
        #region create claim 
        [HttpPost("CreateClaimform")]
        public async Task<ActionResult<int>> CreateClaimform(ClaimsForm model)
        {
            return await _claimService.AddClaimsForm(model);
        }
        #endregion


        #region update claim

        [HttpPut("{claimId}")]
            public async Task<ActionResult<ClaimsDto>> UpdateClaim(string claimId, ClaimsDto model)
            {
                if (claimId != model.ClaimId.ToString())
                {
                    return BadRequest(); // 400 Bad Request if IDs don't match
                }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("Invalid User");
            }
            model.UserId = user?.Id;
            var updatedClaim = await _claimService.UpdateClaims(model);
                if (updatedClaim == null)
                {
                    return NotFound(); // 404 Not Found if claim not found
                }

                return Ok(updatedClaim);
            }
        }
    #endregion
}


