// Afc.Core/Entities/Category.cs
using System.Collections.Generic;

namespace Afc.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? StoreId { get; set; }

        // Navigation properties
        public virtual Store? Store { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}