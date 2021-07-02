using Identity.Application.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models.CustomerClient
{
    public class GetCustomerClientResponse
    {
        [JsonProperty("customer")]
        public CustomerClientResponse Customer { get; set; }

        [JsonProperty("contacts")]
        public List<ContactShortResponse> Contacts { get; set; }
    }

    public class CustomerClientResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("legalType")]
        public LegalTypeEnum LegalType { get; set; }

        [JsonProperty("customerType")]
        public CustomerTypeEnum CustomerType { get; set; }

        [JsonProperty("customerState")]
        public CustomerStateEnum CustomerState { get; set; }

        [JsonProperty("credentialId")]
        public int CredentialId { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("warehouseId")]
        public int? WarehouseId { get; set; }
    }

    public class ContactShortResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("contactType")]
        public ContactTypeResponse ContactType { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("verificationState")]
        public short VerificationState { get; set; }
    }
    public class ContactTypeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
   
}
