using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class CredentialHistory
    {
        public int Id { get; set; }
        public int CredentialId { get; set; }
        public string Password { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
