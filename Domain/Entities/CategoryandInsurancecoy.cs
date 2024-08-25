using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
