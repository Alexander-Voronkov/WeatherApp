using Domain.Entities.DomainServices;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Application.Users.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserUniquenessHelper _uniquenessHelper;
    private readonly IUsersRepository _usersRepository;

    public RegisterCommandHandler(
        IUserUniquenessHelper uniquenessHelper, 
        IUsersRepository usersRepository)
    {
        _uniquenessHelper = uniquenessHelper;
        _usersRepository = usersRepository;
    }

    public async Task<Unit> Handle(
        RegisterCommand request, 
        CancellationToken cancellationToken)
    {
        var email = UserEmail.CreateUserEmail(request.email);
        var pass = UserPassword.CreatePassword(request.pass);

        var user = User.CreateUser(
            email,
            pass,
            _uniquenessHelper);

        await _usersRepository.AddAsync(user, cancellationToken);

        return Unit.Value;
    }
}