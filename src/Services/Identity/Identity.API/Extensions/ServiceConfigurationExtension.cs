using Identity.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            //IConfigurationSection authenticationServiceSettingsConfig = configuration.GetSection("AuthenticationService");
            //AuthenticationSettings authenticationServiceSettings = authenticationServiceSettingsConfig.Get<AuthenticationSettings>();
            //services.Configure<AuthenticationSettings>(authenticationServiceSettingsConfig);
        }
    }
}
