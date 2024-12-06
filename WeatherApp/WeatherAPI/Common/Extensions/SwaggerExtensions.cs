using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WeatherAPI.Common.Extensions;

namespace Wilby.Web.Common.Extensions;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen((options) =>
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var commentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var commentsFile = Path.Combine(baseDirectory, commentsFileName);
            options.IncludeXmlComments(commentsFile);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("http://localhost:7082/connect/authorize"),
                        TokenUrl = new Uri("http://localhost:7082/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "doAll", "doAll" }
                        },
                    },
                }
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }

    internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.OAuthClientId("WeatherUIClientId");
            c.OAuthAppName("WeatherUI");
            c.OAuthClientSecret("WEATHERUISECRET"); 
            
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }

    internal class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorization = context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<Microsoft.AspNetCore.Authorization.IAuthorizeData>()
                .Any();

            if (hasAuthorization)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"
                                }
                            }
                        ] = [
                            "doAll"
                        ]
                    }
                };
            }
        }
    }
}
