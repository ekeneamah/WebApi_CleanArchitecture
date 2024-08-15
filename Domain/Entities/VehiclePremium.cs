using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class VehiclePremium
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string VehicleClass { get; set; }
        [Required]
        public double Premium {  get; set; }
        [Required]
        public string Description { get; set;}
        [Required]
        public string SumInsured { get; set; }
    }
}
