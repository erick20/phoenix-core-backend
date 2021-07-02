using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Models
{
    public class AuthenticationServiceSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int CustomerTokenLifeTime { get; set; }
        public int UserTokenLifeTime { get; set; }

    }
}
