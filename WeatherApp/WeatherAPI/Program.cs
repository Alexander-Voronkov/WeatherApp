using Application;
using Asp.Versioning;
using Carter;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WeatherAPI.Common.ExceptionProblemDetails;
using Wilby.Web.Common.Extensions;
using static Wilby.Web.Common.Extensions.SwaggerExtensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConf) =>
        {
            loggerConf.ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddApplication()
            .AddInfrastructure(builder.Configuration);

        builder.Services.AddAuthorization();

        builder.Services.AddCarter();

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddSwaggerDocumentation();

        builder.Services.AddProblemDetails(x =>
        {
            x.Map<ValidationException>(x => new ValidationExceptionProblemDetails(x));
        });

        builder.Services.AddCors(x => x.AddDefaultPolicy(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseCors();

        app.UseProblemDetails();

        app.UseIdentityServer();

        app.UseAuthorization();

        app.UseSwaggerDocumentation();

        app.MapCarter();

        app.Run();
    }
}