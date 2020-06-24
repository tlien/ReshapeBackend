using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Reshape.Common.DevelopmentTools
{
    /// <summary>
    /// Adds authorization info to generated swagger document.
    /// This includes:
    ///     1. Adding response info.
    ///     2. OAuth2 security scheme (swagger uses this to define what authorization is required to call the API endpoint).
    /// </summary>
    public class AuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                            .Union(context.MethodInfo.GetCustomAttributes(true))
                            .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
            {
                operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse { Description = nameof(HttpStatusCode.Unauthorized) });
                operation.Responses.Add(StatusCodes.Status403Forbidden.ToString(), new OpenApiResponse { Description = nameof(HttpStatusCode.Forbidden) });
            }

            if (authAttributes.Any())
            {
                operation.Security = new List<OpenApiSecurityRequirement>();

                var oauth2SecurityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" },
                };

                operation.Security.Add(new OpenApiSecurityRequirement()
                {
                    [oauth2SecurityScheme] = new[] { "OAuth2" }
                });
            }
        }
    }
}