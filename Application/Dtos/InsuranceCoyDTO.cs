using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class InsuranceCoyDTO
    {

        public string Coy_Name { get; set; }
        public int Coy_id { get; set; }
    }
}
