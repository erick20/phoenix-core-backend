using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class CustomerExtraPermission
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PermissionId { get; set; }
    }
}
