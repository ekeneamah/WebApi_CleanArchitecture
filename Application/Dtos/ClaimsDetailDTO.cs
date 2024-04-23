using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ClaimDetailDTO
    {
        public int Id { get; set; }
        public Guid ClaimsId { get; set; }
        public string UserId { get; set; }
        public string PolicyNo { get; set; }
        public string LossDate { get; set; }
        public string NotifyDate { get; set; }
        public string Reference { get; set; }
        public int? InsuranceCompanyId { get; set; }
        public string NotificationNo { get; set; }
        public string ClaimNo { get; set; }
        public string Status { get; set; }
    }
}
