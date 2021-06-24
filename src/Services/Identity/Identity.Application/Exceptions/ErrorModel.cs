using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Exceptions
{
    public class ErrorModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
