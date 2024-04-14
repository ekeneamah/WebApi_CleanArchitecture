using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ClaimsDto
    {
        public Guid ClaimId { get; set; }
        public Guid PolicyId { get; set; } // Guid for PolicyEntity ID
        public string PolicyNo { get; set; }
        public DateTime LossDate { get; set; }
        public DateTime NotifyDate { get; set; }
        public string ClaimForm { get; set; }
        public string Reference { get; set; }
        public string? UserId { get; set; }
        public required int InsuranceCompanyId { get; set; }
    }
}
