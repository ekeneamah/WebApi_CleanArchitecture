using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ClaimsFormEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Form {  get; set; }
        public string APIEndPoint { get; set; }
        public int Coy_id {  get; set; }
        public string Coy_name { get; set; }
    }
}
