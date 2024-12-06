using MediatR;

namespace Application.Users.Commands.Register;
public record RegisterCommand( 
    string email, 
    string pass)
    : IRequest<Unit>;