using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class Credential
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public DateTime? ResetDate { get; set; }
        public short CustomerState { get; set; }
    }
}
