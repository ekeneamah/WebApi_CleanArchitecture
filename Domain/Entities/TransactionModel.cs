using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TransactionVerificationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public TransactionData Data { get; set; }
    }

    public class TransactionData
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Reference { get; set; }
        public string Domain { get; set; }
        public string GatewayResponse { get; set; }
        public string Channel { get; set; }
        public string IpAddress { get; set; }
        public TransactionLog Log { get; set; }
    }

    public class TransactionLog
    {
        public int TimeSpent { get; set; }
        public int Attempts { get; set; }
        public string Authentication { get; set; }
        public int Errors { get; set; }
        public bool Success { get; set; }
        public string Channel { get; set; }
        public TransactionHistory[] History { get; set; }
    }

    public class TransactionHistory
    {
        public string? Type { get; set; }
        public string? Message { get; set; }
        public int Time { get; set; }
    }
}
