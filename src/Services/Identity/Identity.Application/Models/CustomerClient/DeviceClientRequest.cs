using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models.CustomerClient
{
    public class DeviceClientRequest
    {        
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("fsmToken")]
        public string FsmToken { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
    }
}
