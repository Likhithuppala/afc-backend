// Afc.Core/Entities/Item.cs
using System;
using System.Collections.Generic;

namespace Afc.Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public int StoreId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSpecial { get; set; }
        public DateTime? SpecialExpiry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual Store Store { get; set; } = null!;
    }
}