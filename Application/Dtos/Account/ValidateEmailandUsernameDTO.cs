using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Account
{
    public class ValidateEmailandUsernameDTO
    {
        [Required]
        public required string  Email { get; set; }
        [Required]
        public required string UserName { get; set; }
    }
}
