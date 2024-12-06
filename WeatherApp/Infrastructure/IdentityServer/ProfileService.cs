using Application.Users.Queries.GetUserById;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MediatR;
using System.Security.Claims;

namespace Infrastructure.IdentityServer;

internal class ProfileService : IProfileService
{
    private readonly ISender _sender;

    public ProfileService(
        ISender sender)
    {
        _sender = sender;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var parseResult = int.TryParse(context.Subject.GetSubjectId(), out var userId);

        if (parseResult == false)
        {
            return;
        }

        var userResult = await _sender.Send(new GetUserProfileByIdQuery(userId));

        var userClaims = new List<Claim> { new Claim("email", userResult.Email), new Claim("userid", userId.ToString()) };

        context.IssuedClaims.AddRange(userClaims);
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.FromResult(context.IsActive);
    }
}