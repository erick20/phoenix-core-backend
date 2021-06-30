using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1
{
    public class AccountLimitV1Reposne
    {
        [JsonProperty("currencyId")]
        public short CurrencyId { get; set; }

        [JsonProperty("minLimit")]
        public decimal MinLimit { get; set; }

        [JsonProperty("maxLimit")]
        public decimal MaxLimit { get; set; }
    }
}
