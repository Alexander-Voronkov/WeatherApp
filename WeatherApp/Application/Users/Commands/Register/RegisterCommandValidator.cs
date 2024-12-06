using FluentValidation;

namespace Application.Users.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        this.RuleFor(x => x.email)
            .NotEmpty();

        this.RuleFor(x => x.pass)
            .NotEmpty();
    }
}