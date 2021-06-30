using Identity.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Filters
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizationAttribute>()
                .Select(attr => attr)
                .Distinct();

            if (requiredScopes.Any())
            {
                #region Error message config

                string contentType = "application/json";
                var properties = new Dictionary<string, OpenApiSchema>();
                var unauthorizedResponse = new OpenApiResponse();
                var forbiddenResponse = new OpenApiResponse();

                var openApiSchema = new OpenApiSchema
                {
                    Type = "object",
                    AdditionalPropertiesAllowed = true,
                    Properties = properties
                };

                //properties.Add("message", new OpenApiSchema() { Type = "string" });
                properties.Add("key", new OpenApiSchema() { Type = "string" });

                unauthorizedResponse.Content.Add(contentType, new OpenApiMediaType
                {
                    Schema = openApiSchema
                });
                unauthorizedResponse.Description = "Unauthorized";

                operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), unauthorizedResponse);

                forbiddenResponse.Content.Add(contentType, new OpenApiMediaType
                {
                    Schema = openApiSchema
                });

                forbiddenResponse.Description = "Forbidden";
                operation.Responses.Add(StatusCodes.Status403Forbidden.ToString(), forbiddenResponse);
                #endregion

                var oAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ oAuthScheme ] = new List<string>()
                    }
                };
            }
        }
    }
}
