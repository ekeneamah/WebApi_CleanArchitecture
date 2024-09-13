using Application.Common;
using Application.Dtos.Account;
using Application.Interfaces.Authentication;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthResponse _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<AppUser> _userManager;
   
        public AuthController(IAuthResponse authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        #region SetRefreshTokenInCookies
        [EndpointDescription("SetRefreshedTokenInCookies")]
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
        public async Task<ActionResult<ApiResult<AuthResponse>>> SignUpAsync([FromBody]SignUp model)
        {
            var orgin = Request.Headers["origin"];
            var result = await _authService.SignUpAsync(model, orgin);
            if (result.Success)
            {
                //store the refresh token in a cookie
                SetRefreshTokenInCookies(result.Data?.RefreshToken??"invalid", result.Data!.RefreshTokenExpiration);
            }
          
            return HandleOperationResult(result);
        }

        #endregion

        #region Login Endpoint

        [HttpPost("login")]
        public async Task<ActionResult<ApiResult<AuthResponse>>> LoginAsync([FromBody] Login model)
        {
            var result = await _authService.LoginAsync(model);

            if (result.Success && result.Data?.RefreshToken != null)
            {
                //check if the user has a refresh token or not , to store it in a cookie
                SetRefreshTokenInCookies(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);
            }
         
            return HandleOperationResult(result);
        }

        #endregion

        #region AssignRole Endpoint
/// <summary>
/// 
/// </summary>
/// <param name="model"></param>
/// <returns></returns>
        [HttpPost("role")]
        public async Task<ActionResult<ApiResult<AssignRolesDto>>> AddRoleAsync([FromBody] AssignRolesDto model)
        {
            var result = await _authService.AssignRolesAsync(model);

            if (!string.IsNullOrEmpty(result))
                return HandleOperationResult(ApiResult<AssignRolesDto>.Failed(result));

            return HandleOperationResult(ApiResult<AssignRolesDto>.Successful(model));
        }

        #endregion

        #region RefreshTokenCheck Endpoint

        [HttpGet("refresh-token")]
        public async Task<ActionResult<ApiResult<AuthResponse>>> RefreshTokenCheckAsync()
        {
            var refreshToken = Request.Cookies["refreshTokenKey"];

            var result = await _authService.RefreshTokenCheckAsync(refreshToken);
            return HandleOperationResult(result);
        }

        #endregion

        #region RevokeTokenAsync

        [HttpPost("revoke-token")]
        public async Task<ActionResult<ApiResult<bool>>> RevokeTokenAsync(RevokeToken model)
        {
            var refreshToken = model.Token ?? Request.Cookies["refreshTokenKey"];

            //check if there is no token
            if (string.IsNullOrEmpty(refreshToken))
                return HandleOperationResult(ApiResult<bool>.Failed("Invalid Token"));

            var result = await _authService.RevokeTokenAsync(refreshToken);
            return HandleOperationResult(result);
        }

        #endregion

        #region ConfirmOTP
        [HttpPost("confirm-otp")]
        [Authorize]
        public async Task<ActionResult<ApiResult<string>>> ConfirmOTPAsync([FromBody] OtpDto otp)
        {
            try
            {
                // Retrieve user from UserManager using the UserId claim
                var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value);
                if (user == null)
                    return HandleOperationResult(ApiResult<string>.Failed("Invalid User"));

                    // return BadRequest("Invalid User");

                // Prepare the model for OTP confirmation
                VerifyOtpDto model = new()
                {
                    UserId = user.Id,
                    Otp = otp.Otp,
                    Token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(),
                    Email = user.Email
                };

                // Call the service to confirm OTP
                var result = await _authService.ConfirmOTPAsync(model);
                return HandleOperationResult(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                 _logger.LogError(ex, "An error occurred while confirming OTP.");

                // Return a generic error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        #endregion

        #region Resend OTP
        [HttpPost("resend-otp")]
        [Authorize]
        public async Task<ActionResult<ApiResult<string>>> ResendOTPAsync()
        {

            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return HandleOperationResult( ApiResult<string>.Failed("Invalid User"));

            VerifyOtpDto model = new()
            {
                UserId = user.Id,
                Token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(),
                Email = user.Email
            };


            return HandleOperationResult(await _authService.ResendOTPAsync(model));
        }
        #endregion

        #region signout
        [HttpGet("signout")]
        public async Task<ActionResult<ApiResult<string>>> Signout()
        {
            return HandleOperationResult(await _authService.Signout());
        }
        #endregion
        #region signout

        [HttpDelete("users")]
        public async Task<ActionResult<ApiResult<string>>> DeleteAllUser()
        {
            return HandleOperationResult(await _authService.DeleteAllUserAsync());
        }
        #endregion
        #region validateEmail
        [HttpPost("validate-email-username")]
        public async Task<ActionResult<ApiResult<string>>> ValidateEmailAndUsername([FromBody] ValidateEmailandUsernameDTO validateEmailandUsernameDTO)
        {
            var result = await _authService.ValidateEmailandUsernameAsync(validateEmailandUsernameDTO);

            return HandleOperationResult(result);
        }
        #endregion
        #region forgot password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResult<string>.Failed("Invalid input data."));
            }

            var result = await _authService.ForgotPasswordAsync(model.Email);

            if (result.Success)
            {
                return Ok(ApiResult<string>.Successful(null, "Password reset email has been sent."));
            }

            return BadRequest(ApiResult<string>.Failed(result.Message));
        }
        #endregion
        #region reset password
        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResult<string>>> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var result = await _authService.ResetPasswordWithCodeAsync(model.Email, model.ResetCode, model.NewPassword);
            
            return HandleOperationResult(result);
        }
        #endregion
        #region validate rest otp
        [HttpPost("validate-reset-code")]
        public async Task<ActionResult<ApiResult<string>>> ValidateResetCode([FromBody] ValidateCodeRequestDto request)
        {
            var isValid = _passwordService.ValidateResetCode(request.Email, request.Code);
            if (!isValid)
                return BadRequest(ApiResult<string>.ErrorResult("Invalid or expired code."));

            return Ok(ApiResult<string>.SuccessResult(null, "Code validated successfully."));
        }

        #endregion
    }
}