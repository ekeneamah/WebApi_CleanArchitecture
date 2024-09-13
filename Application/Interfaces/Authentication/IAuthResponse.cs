using Application.Common;
using Application.Dtos.Account;
using Azure;

namespace Application.Interfaces.Authentication
{
    public interface IAuthResponse
    {
        //For signup logic
        Task<ApiResult<AuthResponse>> SignUpAsync(SignUp model, string orgin);

        //For login logic
        Task<ApiResult<AuthResponse>> LoginAsync(Login model);

        //for addroles logic
        Task<string> AssignRolesAsync(AssignRolesDto model);

        //for checking if the sent token is valid
        Task<ApiResult<AuthResponse>> RefreshTokenCheckAsync(string token);

        // for revoking refreshrokens
        Task<ApiResult<bool>> RevokeTokenAsync(string token);

        Task<ApiResult<string>> ConfirmOTPAsync(VerifyOtpDto model);
        Task<ApiResult<string>> ResendOTPAsync(VerifyOtpDto model);
        Task<ApiResult<string>> Signout();
        Task<ApiResult<string>> DeleteAllUserAsync();
        Task<ApiResult<string>> ValidateEmailandUsernameAsync(ValidateEmailandUsernameDTO  validateEmailandUsernameDTO);
        Task<ApiResult<string>> ForgotPasswordAsync(string email);
        Task<ApiResult<string>> ResetPasswordWithCodeAsync(string email, string code, string newPassword);
        Task<ApiResult<string>> ValidateResetCode(string email, string code);
        
    }
}