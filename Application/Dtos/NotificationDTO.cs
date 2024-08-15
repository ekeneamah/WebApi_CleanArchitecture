using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class NotificationDto
    {
        public string NotificationNo { get; set; }
        public string ClaimNo { get; set; }
        public string PolicyNo { get; set; }
        public DateTime LossDate { get; set; }
        public DateTime NotifyDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid? ClaimsId { get; set; }
    }
}
