// Afc.Core/Entities/OrderItem.cs
using System;

namespace Afc.Core.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public int StoreId { get; set; }
        public bool? IsDelivered { get; set; }
        public DateTime? DeliveredAt { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}