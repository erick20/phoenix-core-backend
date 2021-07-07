using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models.Authentication
{
    public class RefreshToken
    {
        public int CustomerId { get; set; }
        public DateTime ExpDate { get; set; }
        public string Magic { get; set; }
        public int Version { get; set; }
    }
}
