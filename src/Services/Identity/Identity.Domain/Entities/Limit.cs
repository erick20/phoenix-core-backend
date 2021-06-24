using System;
using System.Collections.Generic;

#nullable disable

namespace Identity.Domain.Entities
{
    public partial class Limit
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal? MinLimit { get; set; }
        public decimal? MaxLimit { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
