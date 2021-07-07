using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Models
{
    public class CustomerClientSettings : IClientServiceBase
    {
        public string BaseAddress { get; set; }
        public string GetCustomerByContact { get; set; }
        public string GetCustomerById { get; set; }
        public string AddOrUpdateDevice { get; set; }
    }
}
