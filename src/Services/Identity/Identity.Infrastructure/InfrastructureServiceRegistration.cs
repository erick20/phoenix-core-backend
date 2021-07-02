using Identity.Application.Contracts.Persistence;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Identity.Infrastructure.Services;
using Identity.Application.Contracts.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Identity.Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using Identity.Application.Helpers;

namespace Identity.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString, IConfiguration configuration)//IOptions<AuthenticationServiceSettings> authenticationServiceSettings)//IConfiguration configuration)
        {
            AuthenticationServiceSettings authenticationServiceSettings = configuration.Get<AuthenticationServiceSettings>();

            #region Jwt Configs
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authenticationServiceSettings.Issuer,
                            ValidateAudience = true,
                            ValidAudience = authenticationServiceSettings.Audience,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            IssuerSigningKey = CryptHelper.GetSymmetricSecurityKey(authenticationServiceSettings.Key),
                            ValidateIssuerSigningKey = true,
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
            #endregion

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IUserContextService, UserContextService>();

            return services;
        }
    }
}
