// Afc.Core/Entities/User.cs
using System;
using System.Collections.Generic;

namespace Afc.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdminPassword { get; set; }
        public int? StoreId { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public string? CurrentOtp { get; set; }
        public DateTime? OtpExpiresAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Username { get; set; }

        // Navigation properties
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual Role Role { get; set; } = null!;
        public virtual Store? Store { get; set; }
    }
}