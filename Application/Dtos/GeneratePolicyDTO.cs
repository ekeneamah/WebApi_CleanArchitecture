using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class GeneratePolicyDto
    {
        public bool IsOrg { get; set; }
        public int Kycid { get; set; }
        public List<SectionDto> Sections { get; set; }
        public int ProductId { get; set; }
        public object Userid { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Token { get; set; }
    }
}
