using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate.ValueObjects;
using Domain.Entities.DomainServices;
using Domain.UserAggregate.BusinessRules;

namespace Domain.UserAggregate;

public class User : BaseEntity<UserId, int>, IAggregateRoot
{
    private UserEmail _email;

    private UserPassword _password;

    private DateTimeOffset _registeredAt;

    private User(
        UserEmail email,
        UserPassword password)
    {
        _email = email;
        _password = password;
    }

    private User()
    {
        // ef
    }

    public static User CreateUser(
        UserEmail email,
        UserPassword password,
        IUserUniquenessHelper uniquenessHelper)
    {
        var user = new User(
            email,
            password);

        user.CheckRule(new UserHasToBeUniqueBusinessRule(email, uniquenessHelper));

        return user;
    }
}