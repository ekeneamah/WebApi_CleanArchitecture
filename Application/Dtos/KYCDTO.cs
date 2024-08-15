using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class Kycdto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string IdentityType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime FromExpiryDate { get; set; }

        [Required]
        public DateTime ToExpiryDate { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        public string UserId { get; set; }
    }
}
