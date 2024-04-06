using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class InsuranceCoy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Coy_Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Coy_Name { get; set; }
    }
}