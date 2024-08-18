using Application.Common;
using Application.Dtos;
using Application.Interfaces.Content.Products;
using Application.Interfaces.Content.UserProfiles;
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
    public class UserProfileController : BaseController
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
        public async Task<ActionResult<ApiResult<List<UserProfileDto>>>> GetUserPrifiles()
        {
            return HandleOperationResult(await _profileservice.GetAll());
        }
        #endregion
        #region getbyuserid
        [HttpGet("GetUserid")]
        public async Task<ActionResult<ApiResult<UserProfileDto>>> GetUserPrifilebyid()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            return HandleOperationResult(await _profileservice.GetProfilebyUserid(user.Id));
        }
        #endregion

        #region create profile
        [HttpPut]
        public async Task<ActionResult<ApiResult<UserProfileDto>>> UpdateProfile( UserProfileDto u)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var orgin = Request.Headers["origin"];
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            u.UserId = user.Id;
            var result = await _profileservice.UpdateUser(u);
            return HandleOperationResult(result);
        }
        #endregion create profile

        #region update only logo
        [HttpPut("{id}/profilepix")]
        public async Task<ActionResult<ApiResult<bool>>> UpdateProfileImage(int id, IFormFile profileImageFile, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
           
            if (!string.IsNullOrEmpty(user.ProfilePix))
            {
                string wwwrootPathold = webHostEnvironment.WebRootPath;
                string logoImagePathold = Path.Combine(wwwrootPathold, user.ProfilePix.TrimStart('~', '/'));
                if (System.IO.File.Exists(logoImagePathold))
                {
                    System.IO.File.Delete(logoImagePathold);
                }
            }

            // Save logo image file to wwwroot/Images folder
            string wwwrootPath = webHostEnvironment.WebRootPath;
            string imagesFolder = Path.Combine(wwwrootPath, "images");
            string logoImageName = $"{Guid.NewGuid()}_proflepix_{profileImageFile.FileName}";
            string logoImagePath = Path.Combine(imagesFolder, logoImageName);

            using (var stream = new FileStream(logoImagePath, FileMode.Create))
            {
                await profileImageFile.CopyToAsync(stream);
            }

            // Set logo image URL
            user.ProfilePix = $"~/images/{logoImageName}";

            // Update InsuranceCoy in the database with the new logo image URL
           

            return HandleOperationResult(ApiResult<bool>.Successful(await _profileservice.Update_UserProfilePix(user.ProfilePix,user.Id)));
        }

        #endregion

        #region delete profile pix
        [HttpDelete("{id}/profilepix")]
        public async Task<ActionResult<ApiResult<bool>>> DeleteProfileImage(int id, IFormFile profileImageFile, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");

            if (!string.IsNullOrEmpty(user.ProfilePix))
            {
                string wwwrootPathold = webHostEnvironment.WebRootPath;
                string logoImagePathold = Path.Combine(wwwrootPathold, user.ProfilePix.TrimStart('~', '/'));
                if (System.IO.File.Exists(logoImagePathold))
                {
                    System.IO.File.Delete(logoImagePathold);
                }
            }

          

            // Set logo image URL
            user.ProfilePix = null;

            // Update InsuranceCoy in the database with the new logo image URL


            return HandleOperationResult(ApiResult<bool>.Successful(await _profileservice.Update_UserProfilePix(user.ProfilePix, user.Id)));
        }

        #endregion



    }
}
