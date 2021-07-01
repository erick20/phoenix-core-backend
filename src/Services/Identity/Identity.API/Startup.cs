using Identity.API.Extensions;
using Identity.Application;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API
{
    //   Run This from Identity.API Project, after creating change dbContext into infra layer
    //   Scaffold-DbContext "Server=34.107.123.66; Port=5432; Database=authorization; User Id=api_user; Password=vDsweiobeqo9ixOA" Npgsql.EntityFrameworkCore.PostgreSQL -Project "Identity.Domain" -o Entities -ContextDir DBContext -Context Persisence -ContextNamespace Identity.Infrastructure.Persistence

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();

            services.AddInfrastructureServices(Configuration);

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseErrorHandlingMiddleware();

            app.UseCors(builder =>
          builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomSwagger();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
