using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate.BusinessRules;

namespace Domain.UserAggregate.ValueObjects;

public record UserEmail : ValueObject
{
    public string Value { get; }

    private UserEmail(
        string value)
    {
        Value = value;
    }

    public static UserEmail CreateUserEmail(
        string value)
    {
        var userEmail = new UserEmail(value);

        userEmail.CheckRule(new UserEmailHasToBeValidBusinessRule(userEmail));

        return userEmail;
    }

    private UserEmail()
    {
        // ef
    }
}