using MediatR;

namespace Application.Users.Queries.GetUserById;
public record GetUserProfileByIdQuery(
    int userId) : IRequest<UserProfileDto>;