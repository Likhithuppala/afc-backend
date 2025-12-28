// Afc.Core/Entities/Store.cs
using System.Collections.Generic;

namespace Afc.Core.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string? BankDetails { get; set; } // JSON

        // Navigation properties
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}