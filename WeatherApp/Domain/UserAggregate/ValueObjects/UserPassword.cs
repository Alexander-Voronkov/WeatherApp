using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate.BusinessRules;
using System.Security.Cryptography;
using System.Text;

namespace Domain.UserAggregate.ValueObjects;
public record UserPassword : ValueObject
{
    public string Value { get; }

    private UserPassword(string value)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(value);
        var hashedPass = sha256.ComputeHash(bytes);

        Value = Convert.ToBase64String(hashedPass);
    }

    private UserPassword()
    {
        // ef
    }

    public static UserPassword CreatePassword(
        string value)
    {
        var pass = new UserPassword(value);

        pass.CheckRule(new UserPasswordHasToBeValidBusinessRule(value));

        return pass;
    }
}