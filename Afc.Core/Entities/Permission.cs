// Afc.Core/Entities/Permission.cs
using System.Collections.Generic;

namespace Afc.Core.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;

        // Navigation property for many-to-many
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}