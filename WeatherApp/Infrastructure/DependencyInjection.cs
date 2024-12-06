using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.IdentityServer;
using Application.Common.Interfaces.Data;
using Infrastructure.Data;
using Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Application.Services;
using Infrastructure.Services;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Application.Users;
using IdentityModel;

namespace Infrastructure;

public static class DependencyInjection
{
    private const string ConnectionStringSectionName = "Default";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        var connectionString = configuration.GetConnectionString(ConnectionStringSectionName)!;

        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(connectionString);
            opt.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryApiResources(IdentityServerConfig.GetApis())
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryPersistedGrants()
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();

        services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

        services.AddScoped<ISqlConnectionFactory>(provider =>
            new SqlConnectionFactory(connectionString));

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
             .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
             {
                 x.Authority = "http://localhost:7082";
                 x.RequireHttpsMetadata = false;
                 x.NameClaimType = JwtClaimTypes.Subject;
             });

        services.AddHttpClient("WeatherAPIClient", x =>
        {
            x.BaseAddress = new Uri("http://api.openweathermap.org");
            x.DefaultRequestHeaders.Add("x-api-key", configuration["WeatherAPIKey"]);
        });

        services.AddScoped<IWeatherService, WeatherService>();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContextAccessor, UserContextAccessor>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "WeatherAPIInstance";
        });

        return services;
    }
}