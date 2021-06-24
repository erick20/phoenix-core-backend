using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            Limits = new HashSet<Limit>();
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleGroupId { get; set; }
        public int WarehouseTypeId { get; set; }

        public virtual RoleGroup RoleGroup { get; set; }
        public virtual ICollection<Limit> Limits { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
