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
            IConfigurationSection authenticationServiceSettingsConfig = configuration.GetSection("AuthenticationService");
            AuthenticationServiceSettings authenticationServiceSettings = authenticationServiceSettingsConfig.Get<AuthenticationServiceSettings>();
            services.Configure<AuthenticationServiceSettings>(authenticationServiceSettingsConfig);
        }
    }
}
