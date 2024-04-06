using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Account
{
    public class VerifyOTPDto
    {
        public string? UserId { get; set; }

        public string? Email { get; set; }
        public string OTP { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class OTPDto
    {
        public string OTP { get; set; }
    }
}
