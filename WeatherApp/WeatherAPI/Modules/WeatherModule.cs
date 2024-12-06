using Application.Users.Commands.Register;
using Application.Weather.Queries.GetSearchHistory;
using Application.Weather.Queries.GetWeatherByCity;
using Asp.Versioning;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace WeatherAPI.Modules;

public class WeatherModule : CarterModule
{
    private const string WeatherBasePath = "";

    public WeatherModule() : base(WeatherBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var mapGroup = app.MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        mapGroup.MapPost("/register", async (RegisterCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result;
        })
        .MapToApiVersion(1);

        mapGroup.MapGet("/search/{city}", async (string city, ISender sender) =>
        {
            var query = new GetWeatherByCityQuery(city);

            var result = await sender.Send(query);

            return result;
        })
        .MapToApiVersion(1)
        .RequireAuthorization();

        mapGroup.MapGet("/history", async (ISender sender) =>
        {
            var result = await sender.Send(new GetSearchHistoryQuery());

            return result;
        })
        .MapToApiVersion(1)
        .RequireAuthorization();
    }
}
