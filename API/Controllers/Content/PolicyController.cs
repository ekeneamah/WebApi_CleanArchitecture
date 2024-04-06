using Application.Dtos;
using Application.Interfaces.Content.Policy;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicy _policyService;
        private readonly UserManager<AppUser> _userManager;
        public PolicyController(IPolicy policyService,UserManager<AppUser> userManager ) {
            _policyService = policyService;
            _userManager = userManager;
        }
        #region create policy
        [HttpPost]
        public ActionResult<int> Create(PolicyDTO policyDTO)
        {
            return Ok(_policyService.AddPolicy(policyDTO));
        }
        #endregion

        #region get policy customer id
        [HttpGet("GetMyPolicies")]
        public async Task<ActionResult> GetMyPolicies()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            return Ok(_policyService.GetAllPolicyByUserId(user.Id));
        }
        #endregion

        #region get policy customer id
        [HttpPut("Update")]
        public async Task<ActionResult> UpdatePolicies(PolicyDTO policy)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            policy.UserId = user.Id;
            return Ok(_policyService.UpdatePolicy(policy));
        }
        #endregion
    }
}
