using Application.Dtos.Account;
using Application.Interfaces.Authentication;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace CleanaArchitecture1.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthResponse _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthResponse authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        # region SetRefreshTokenInCookies

        private void SetRefreshTokenInCookies(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            //cookieOptionsExpires = DateTime.UtcNow.AddSeconds(cookieOptions.Timeout);

            Response.Cookies.Append("refreshTokenKey", refreshToken, cookieOptions);
        }

        #endregion

        #region SignUp Endpoint

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUpAsync(SignUp model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var orgin = Request.Headers["origin"];
            var result = await _authService.SignUpAsync(model, orgin);

            if (!result.ISAuthenticated)
                return BadRequest(result.Message);

            //store the refresh token in a cookie
            SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        #endregion

        #region Login Endpoint

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.ISAuthenticated)
                return BadRequest(result.Message);

            //check if the user has a refresh token or not , to store it in a cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        #endregion

        #region AssignRole Endpoint
/// <summary>
/// 
/// </summary>
/// <param name="model"></param>
/// <returns></returns>
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(AssignRolesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AssignRolesAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        #endregion

        #region RefreshTokenCheck Endpoint

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshTokenCheckAsync()
        {
            var refreshToken = Request.Cookies["refreshTokenKey"];

            var result = await _authService.RefreshTokenCheckAsync(refreshToken);

            if (!result.ISAuthenticated)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion

        #region RevokeTokenAsync

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeToken model)
        {
            var refreshToken = model.Token ?? Request.Cookies["refreshTokenKey"];

            //check if there is no token
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required");

            var result = await _authService.RevokeTokenAsync(refreshToken);

            //check if there is a problem with "result"
            //if (!result)
            //    return BadRequest("Token is Invalid");

            return Ok("Done Logout");
        }

        #endregion

        #region ConfirmOTP
        [HttpPost("confirm-otp")]
        [Authorize]
        public async Task<IActionResult> ConfirmOTPAsync(OTPDto otp)
        {
           
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t=>t.Type=="UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");

            VerifyOTPDto model= new();
            model.UserId = user.Id;
            model.OTP = otp.OTP;
            model.Token =  HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            model.Email = user.Email;
           

            return Ok(await _authService.ConfirmOTPAsync(model));
        }
        #endregion

        #region Resend OTP
        [HttpPost("resend-otp")]
        [Authorize]
        public async Task<IActionResult> ResendOTPAsync()
        {

            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");

            VerifyOTPDto model = new()
            {
                UserId = user.Id,
                Token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(),
                Email = user.Email
            };


            return Ok(await _authService.ConfirmOTPAsync(model));
        }
        #endregion

        #region signout
        [HttpGet("signout")]
        public async Task<IActionResult> Signout()
        {

            return Ok(await _authService.Signout());
        }
        #endregion
    }
}