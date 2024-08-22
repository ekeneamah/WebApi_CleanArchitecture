using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
        public  string? UserId { get; set; }
    }
}
