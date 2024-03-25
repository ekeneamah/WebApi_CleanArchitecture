using Application.Interfaces.Content.Products;
using Application.Interfaces.Content.UserProfiles;
using Domain.Models;
using Infrastructure;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfile _profileservice;

        public UserProfileController(IUserProfile profileService)
        {
            _profileservice = profileService;
        }
        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserProfile>>> GetUserPrifiles()
        {
            return Ok(await _profileservice.GetAll());
        }
        #endregion
        #region getbyuserid
        [HttpGet("GetUserid/{userid}")]
        public async Task<ActionResult<UserProfile>> GetUserPrifilebyid(string userid)
        {
            return Ok(await _profileservice.GetById(userid));
        }
        #endregion
    }
}
