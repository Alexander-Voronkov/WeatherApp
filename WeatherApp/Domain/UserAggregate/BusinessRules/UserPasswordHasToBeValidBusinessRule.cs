using Domain.Common.EntitiesAbstractions;
using System.Text.RegularExpressions;

namespace Domain.UserAggregate.BusinessRules;
internal class UserPasswordHasToBeValidBusinessRule : IBusinessRule
{
    public string Message => $"User password`s length is less than {_minLength} or more than {_maxLength} symbols or does not contain any special symbol.";

    private const int _minLength = 8;

    private const int _maxLength = 16;

    private string _password;

    private static readonly string PasswordPattern = $@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{{}};':""\\|,.<>\/?])[A-Za-z\d!@#$%^&*()_+\-=\[\]{{}};':""\\|,.<>\/?]{{{_minLength},{_maxLength}}}$";
    private static readonly Regex PasswordRegex = new Regex(PasswordPattern, RegexOptions.Compiled);

    internal UserPasswordHasToBeValidBusinessRule(
        string password)
    {
        _password = password;
    }

    public bool IsBroken()
    {
        return !PasswordRegex.IsMatch(_password);
    }
}
