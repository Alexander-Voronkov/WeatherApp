using MediatR;
using Application.Users.Queries.Authenticate;
using IdentityServer4.Validation;
using IdentityServer4.Models;

namespace Infrastructure.IdentityServer;
internal class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly ISender _sender;

    public ResourceOwnerPasswordValidator(ISender sender)
    {
        _sender = sender;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var authenticationResult = await _sender.Send(
            new AuthenticateQuery(context.UserName, context.Password));

        if (authenticationResult.IsFailed)
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "Invalid login or password.");

            return;
        }

        context.Result = new GrantValidationResult(
            authenticationResult.UserId.ToString(),
            "forms");
    }
}