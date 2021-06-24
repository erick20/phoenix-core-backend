using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class PermissionGroup
    {
        public PermissionGroup()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
