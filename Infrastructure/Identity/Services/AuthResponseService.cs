using Application.Dtos.Account;
using Application.Dtos.Email;
using Application.Interfaces;
using Application.Interfaces.Authentication;
using Domain.Settings;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Email;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Identity.Services
{
    public class AuthResponseService : IAuthResponse
    {
        private readonly AppIdentityContext _appIdentityContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailSender;
        private readonly JWT _Jwt;


        public AuthResponseService(
            AppIdentityContext appIdentityContext,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt,
            IEmailService emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _Jwt = jwt.Value;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _appIdentityContext = appIdentityContext;
        }

        #region create JWT

        // create JWT
        private async Task<JwtSecurityToken> CreateJwtAsync(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            //generate the symmetricSecurityKey by the s.key
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));

            //generate the signingCredentials by symmetricSecurityKey
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //define the  values that will be used to create JWT
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _Jwt.Issuer,
                audience: _Jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_Jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        #endregion create JWT

        #region Generate RefreshToken

        //Generate RefreshToken
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpireOn = DateTime.UtcNow.AddDays(10),
                CreateOn = DateTime.UtcNow
            };
        }

        #endregion Generate RefreshToken

        #region SignUp Method

        //SignUp
        public async Task<AuthResponse> SignUpAsync(SignUp model, string orgin)
        {
            var auth = new AuthResponse();

            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            var userName = await _userManager.FindByNameAsync(model.Username);

            //checking the Email and username
            if (userEmail is not null)
                return new AuthResponse { Message = "Email is Already used ! " };

            if (userName is not null)
                return new AuthResponse { Message = "Username is Already used ! " };

            //fill
            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                OTP = GenerateAndStoreOTP(),
                OtpTimestamp = DateTime.Now.AddHours(1),
                IsActivated = false
               
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            //check result
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }

                return new AuthResponse { Message = errors };
            }

            //assign role to user by default
            await _userManager.AddToRoleAsync(user, "User");

            #region SendVerificationEmail

            // var verificationUri = await SendVerificationEmail(user, orgin);
            await _emailSender.SendEmailAsync(new EmailRequest()
            {
                ToEmail = user.Email,
                FromEmail = "Transcape",
                Body = $@"
        <html>
        <head>
            <style>
                /* Define your CSS styles here */
                .container {{
                    width: 100%;
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #f9f9f9;
                    font-family: Arial, sans-serif;
                }}
                .content {{
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                }}
                .otp-container {{
                    background-color: #007bff;
                    color: #ffffff;
                    padding: 10px 20px;
                    border-radius: 5px;
                    font-size: 24px;
                    margin-top: 20px;
                    text-align: center;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='content'>
                    <h2>Email Verification</h2>
                    <p>Please use the following One-Time Password (OTP) to verify your email:</p>
                    <div class='otp-container'>{user.OTP}</div>
                    <p>OTP was generated on <b> {user.OtpTimestamp}</b> and will expire on <b>{user.OtpTimestamp.AddHours(1)}</b></p>
                    <p>If you did not request this verification, please ignore this email.</p>
                </div>
            </div>
        </body>
        </html>"
            });

       

            #endregion SendVerificationEmail

            var jwtSecurityToken = await CreateJwtAsync(user);

            auth.Email = user.Email;
           // auth.UserId = user.Id;
            auth.Roles = new List<string> { "User" };
            auth.IsAuthenticated = true;
            auth.UserName = user.UserName;
            auth.FirstName = user.FirstName;
            auth.LastName = user.LastName;
            auth.Email = user.Email;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            auth.TokenExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
            auth.Message = "SignUp Succeeded! Otp sent to your email";
            auth.IsActivated = user.IsActivated;
            // create new refresh token
            var newRefreshToken = GenerateRefreshToken();
            auth.RefreshToken = newRefreshToken.Token;
            auth.RefreshTokenExpiration = newRefreshToken.ExpireOn;

            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            return auth;
        }

        #endregion SignUp Method

        #region logout 
        public async Task<string> Signout()
        {
            await _signInManager.SignOutAsync();
            return  "Logged out successfully.";
        }
        #endregion logout

        #region Login Method

        //login
        public async Task<AuthResponse> LoginAsync(Login model)
        {
            var auth = new AuthResponse();

            var user = await _userManager.FindByEmailAsync(model.Email);
            var userpass = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !userpass)
            {
                auth.Message = "Email or Password is incorrect";
                return auth;
            }

            var jwtSecurityToken = await CreateJwtAsync(user);

            var roles = await _userManager.GetRolesAsync(user);


            auth.UserName = user.UserName;
            auth.FirstName = user.FirstName;
            auth.LastName = user.LastName;
            auth.IsEmailConfirmed = user.EmailConfirmed;
            auth.Roles = roles.ToList();
            auth.IsAuthenticated = true;
            auth.UserName = user.UserName;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            auth.TokenExpiresOn = jwtSecurityToken.ValidTo;
            auth.Message = "Login Succeeded ";
            auth.IsActivated = user.IsActivated;

            //check if the user has any active refresh token
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                auth.RefreshToken = activeRefreshToken.Token;
                auth.RefreshTokenExpiration = activeRefreshToken.ExpireOn;
            }
            else
            //in case user has no active refresh token
            {
                var newRefreshToken = GenerateRefreshToken();
                auth.RefreshToken = newRefreshToken.Token;
                auth.RefreshTokenExpiration = newRefreshToken.ExpireOn;

                user.RefreshTokens.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);
            }

            return auth;
        }

        #endregion Login Method

        #region Assign Roles Method

        //Assign Roles
        public async Task<string> AssignRolesAsync(AssignRolesDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.RoleExistsAsync(model.Role);

            //check the user Id and role
            if (user == null || !role)
                return "Invalid ID or Role";

            //check if user is already assiged to selected role
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            //check result
            if (!result.Succeeded)
                return "Something went wrong ";

            return string.Empty;
        }

        #endregion Assign Roles Method

        #region check Refresh Tokens method

        //check Refresh Tokens
        public async Task<AuthResponse> RefreshTokenCheckAsync(string token)
        {
            var auth = new AuthResponse();

            //find the user that match the sent refresh token
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                auth.Message = "Invalid Token";
                return auth;
            }

            // check if the refreshtoken is active
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                auth.Message = "Inactive Token";
                return auth;
            }

            //revoke the sent Refresh Tokens
            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtSecurityToken = await CreateJwtAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            auth.Email = user.Email;
            auth.Roles = roles.ToList();
            auth.IsAuthenticated = true;
            auth.UserName = user.UserName;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            auth.TokenExpiresOn = jwtSecurityToken.ValidTo;
            auth.RefreshToken = newRefreshToken.Token;
            auth.RefreshTokenExpiration = newRefreshToken.ExpireOn;

            return auth;
        }

        #endregion check Refresh Tokens method

        #region revoke Refresh Tokens method

        //revoke Refresh token
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return false;

            // check if the refreshtoken is active
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            //revoke the sent Refresh Tokens
            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();

            await _userManager.UpdateAsync(user);

            return true;
        }

        #endregion revoke Refresh Tokens method

        #region SendVerificationEmail

        private async Task<string> SendVerificationEmail(AppUser user, string origin)
        {
            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // var code = await _userManager.GenerateTwoFactorTokenAsync(user);
            var otp = GenerateAndStoreOTP();

           // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/Auth/confirm-email/";

           // var _enpointUri = new Uri(string.Concat($"{origin}/", route));
           // var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "UserId", user.Id);
           // verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return  otp;
        }
        #endregion SendVerificationEmail
        #region generate otp 
        private string GenerateAndStoreOTP()
        {
            // Generate a random 6-digit OTP
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);

            // Store OTP and timestamp in user-specific storage (e.g., database)
            //user.Otp = otp.ToString();
           // user.OtpTimestamp = DateTime.UtcNow;

            // Save changes to the database
           // await _userManager.UpdateAsync(user);

            return otp.ToString();
        }
        #endregion

        #region confirm otp
        public async Task<string> ConfirmOTPAsync(VerifyOtpDto model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId);
            // code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            if (user == null)
                return "Invalid userid.";

            // Check if OTP is expired (1 hour)
            if (!user.OtpTimestamp.ToString().IsNullOrEmpty() && DateTime.Now.AddHours(1).Subtract(user.OtpTimestamp).TotalHours > 4)
            {
                return "OTP has expired.";
            }

            // Check if OTP matches
            if (user.OTP != model.Otp)
            {
                return "Invalid OTP.";
            }
            user.IsActivated = true;
            await _userManager.UpdateAsync(user);
            

            // Mark the email as confirmed
           // code = Encoding.UTF8.GetString(code);
            await _userManager.ConfirmEmailAsync(user, model.Token);
            return "Email verified successfully. You can now login.";
        }
        #endregion confirm otp
        #region resend otp
        public async Task<string> ResendOTPAsync(VerifyOtpDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            // code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            if (user == null)
                return "Invalid userid.";
            user.OTP = GenerateAndStoreOTP();
            user.OtpTimestamp = DateTime.Now.AddHours(1);
            await _userManager.UpdateAsync(user);

            #region SendVerificationEmail

            // var verificationUri = await SendVerificationEmail(user, orgin);

            await _emailSender.SendEmailAsync(new EmailRequest()
            {
                ToEmail = user.Email,
               FromEmail = "Transcape",
                Body = $@"
        <html>
        <head>
            <style>
                /* Define your CSS styles here */
                .container {{
                    width: 100%;
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #f9f9f9;
                    font-family: Arial, sans-serif;
                }}
                .content {{
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                }}
                .otp-container {{
                    background-color: #007bff;
                    color: #ffffff;
                    padding: 10px 20px;
                    border-radius: 5px;
                    font-size: 24px;
                    margin-top: 20px;
                    text-align: center;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='content'>
                    <h2>Email Verification</h2>
                    <p>Please use the following One-Time Password (OTP) to verify your email:</p>
                    <div class='otp-container'>{user.OTP}</div>
<p>OTP was generated on <b> {user.OtpTimestamp}</b> and will expire on <b>{user.OtpTimestamp.AddHours(4)}</b></p>
                    <p>If you did not request this verification, please ignore this email.</p>
                </div>
            </div>
        </body>
        </html>",
                Subject = "Registration OTP"
            });

            #endregion SendVerificationEmail

            return "OTP re-sent to your e-mail";
        }

        public Task<string> DeleteAllUserAsync()
        {
            throw new NotImplementedException();
        }


        #endregion


    }
}
