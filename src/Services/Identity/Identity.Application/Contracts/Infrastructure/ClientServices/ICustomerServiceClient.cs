using Identity.Application.Models.CustomerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Infrastructure.ClientServices
{
    public interface ICustomerClientService
    {
        Task<GetCustomerClientResponse> GetCustomerByContactAsync(string contact);
        Task<DeviceClientResponse> AddOrUpdateDevice(DeviceClientRequest deviceClientRequest);
    }
}
