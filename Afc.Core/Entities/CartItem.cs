// Afc.Core/Entities/CartItem.cs
using System;

namespace Afc.Core.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime? AddedAt { get; set; }

        // Navigation properties
        public virtual Cart Cart { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}