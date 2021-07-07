using Identity.Application.Contracts.Infrastructure.ClientServices;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Models.CustomerClient;
using Identity.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.ClientServices
{
    internal class CustomerClientService : ClientServiceBase, ICustomerClientService
    {
        IRequestContextService _requestContext;
        private readonly CustomerClientSettings _customerClientSettings;

        public CustomerClientService(HttpClient httpClient, IOptions<CustomerClientSettings> customerClientSettings, ILogger<CustomerClientService> logger, IRequestContextService requestContext)
            : base(httpClient, logger, customerClientSettings)
        {
            _customerClientSettings = customerClientSettings.Value;
            _requestContext = requestContext;
        }

        public async Task<GetCustomerClientResponse> GetCustomerByContactAsync(string contact)
        {
            GetCustomerClientResponse response = await GetAsJsonAsync<GetCustomerClientResponse>(string.Format(_customerClientSettings.GetCustomerByContact, contact), _requestContext.GetAccessToken(), _requestContext.GetInnerToken());

            return response;
        }

        public async Task<DeviceClientResponse> AddOrUpdateDevice(DeviceClientRequest deviceClientRequest)
        {
            DeviceClientResponse deviceClientResponse = await PostAsJsonAsync<DeviceClientResponse>(_customerClientSettings.AddOrUpdateDevice, deviceClientRequest, _requestContext.GetAccessToken(), _requestContext.GetInnerToken());

            return deviceClientResponse; 
        }
    }
}
