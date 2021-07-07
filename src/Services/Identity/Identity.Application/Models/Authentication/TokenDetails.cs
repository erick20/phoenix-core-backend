using Identity.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models.Authentication
{
    public class TokenDetails
    {        
        public int CredentialId { get; set; }
        public int CustomerId { get; set; }
        public int DeviceId { get; set; }
        public int RoleId { get; set; }
        public int RoleGroupId { get; set; }
        public string Magic { get; set; }
        public short CustomerStateId { get; set; }
        public int? WarehouseId { get; set; }
        public int Version { get; set; }
    }
}
