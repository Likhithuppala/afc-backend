// Afc.Core/Entities/SharedItem.cs
using System;

namespace Afc.Core.Entities
{
    public class SharedItem
    {
        public int SharedItemId { get; set; }
        public int ItemId { get; set; }
        public int SharedToStoreId { get; set; }
        public DateTime? RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public string Status { get; set; } = "pending";

        // Navigation properties
        public virtual Item Item { get; set; } = null!;
        public virtual Store SharedToStore { get; set; } = null!;
    }
}