using Application.Common.Behaviours;
using Application.Common.Implementations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities.DomainServices;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(assembly);
            conf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            conf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddScoped<IUserUniquenessHelper, UserUniquenessHelper>();

        return services;
    }
}