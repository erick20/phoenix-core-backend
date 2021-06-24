using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class RoleGroup
    {
        public RoleGroup()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
