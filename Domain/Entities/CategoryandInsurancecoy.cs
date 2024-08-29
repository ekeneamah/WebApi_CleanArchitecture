using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CategoryandInsurancecoy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; } 
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int InsuranceCoyId { get; set; }
        public string CoyName { get;set; }
    }
}
