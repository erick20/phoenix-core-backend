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
        public List<ContactShortClientResponse> Contacts { get; set; }

        [JsonProperty("devices")]
        public List<DeviceClientResponse> Devices { get; set; }
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

    public class ContactShortClientResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("contactType")]
        public ContactTypeClientResponse ContactType { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("verificationState")]
        public short VerificationState { get; set; }
    }
    public class ContactTypeClientResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class DeviceClientResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("fsmToken")]
        public string FsmToken { get; set; }

        [JsonProperty("lastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
    }
}

