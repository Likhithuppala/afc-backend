// Afc.Core/Entities/Payment.cs
using System;

namespace Afc.Core.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string? PaymentGatewayId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string Status { get; set; } = "success";
        public DateTime? PaidAt { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
    }
}