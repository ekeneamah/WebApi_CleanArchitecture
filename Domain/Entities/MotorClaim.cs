using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MotorClaim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string VehicleRegNo { get; set; }

        public DateTime AccidentDate { get; set; }

        public string AccidentDescribe { get; set; }

        public string AccidentPlace { get; set; }

        public string WeatherCondition { get; set; }

        public int LossTypeCode { get; set; } = 1;
        public  string? User_Id { get; set; }
    }
}
