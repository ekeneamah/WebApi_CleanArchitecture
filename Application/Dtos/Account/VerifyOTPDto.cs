﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Account
{
    public class VerifyOtpDto
    {
        public string? UserId { get; set; }

        public string? Email { get; set; }
        public string Otp { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class OtpDto
    {
        public string Otp { get; set; }
    }
}
