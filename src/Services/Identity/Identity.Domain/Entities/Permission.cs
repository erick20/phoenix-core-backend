using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string ActionKey { get; set; }
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public int GroupId { get; set; }

        public virtual PermissionGroup Group { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
