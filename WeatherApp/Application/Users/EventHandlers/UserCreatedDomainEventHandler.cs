using MediatR;
using Microsoft.Extensions.Logging;
using Wilby.Domain.Events;

namespace Application.Users.EventHandlers;
public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly ILogger _logger;

    public UserCreatedDomainEventHandler(
        ILogger<UserCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User with id: {notification.Id.Value} has just been created.");
        return Task.CompletedTask;
    }
}
