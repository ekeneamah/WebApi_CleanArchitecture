using Application.Dtos;
using Application.Interfaces.Content.Products;
using Application.Interfaces.Content.UserProfiles;
using Domain.Models;
using Infrastructure;
using Infrastructure.Content.Services;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfile _profileservice;
        private readonly UserManager<AppUser> _userManager;


        public UserProfileController(IUserProfile profileService,UserManager<AppUser> userManager)
        {
            _profileservice = profileService;
            _userManager = userManager;
           
        }
        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserProfileDto>>> GetUserPrifiles()
        {
            return Ok(await _profileservice.GetAll());
        }
        #endregion
        #region getbyuserid
        [HttpGet("GetUserid")]
        public async Task<ActionResult<UserProfileDto>> GetUserPrifilebyid()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            return Ok(await _profileservice.GetById(user.Id));
        }
        #endregion

        #region create profile
        [HttpPut]
        public async Task<ActionResult> UpdateProfile( UserProfileDto u)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var orgin = Request.Headers["origin"];
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            u.UserId = user.Id;
            var result = await _profileservice.Add(u);
            return new JsonResult(result);
        }
        #endregion create profile

       
      
    }
}
