using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class GeneratePolicyDTO
    {
        public bool isOrg { get; set; }
        public int kycid { get; set; }
        public List<SectionDTO> sections { get; set; }
        public int ProductId { get; set; }
        public object Userid { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Token { get; set; }
    }
}
