// Afc.Core/Entities/Order.cs
using System;
using System.Collections.Generic;

namespace Afc.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string TokenNumber { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; } = "pending";

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}