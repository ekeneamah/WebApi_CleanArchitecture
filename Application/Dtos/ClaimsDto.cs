using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ClaimsDto
    {
       
        public string PolicyNo { get; set; }
        public DateOnly LossDate { get; set; }
        public DateOnly NotifyDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string? UserId { get; set; }
        public Guid? ClaimId { get; set; }
    }

    public class ClaimsErrorDto
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
