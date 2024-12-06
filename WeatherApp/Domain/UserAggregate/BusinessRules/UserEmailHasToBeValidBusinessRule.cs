using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate.ValueObjects;
using System.Text.RegularExpressions;

namespace Domain.UserAggregate.BusinessRules;
internal class UserEmailHasToBeValidBusinessRule : IBusinessRule
{
    public string Message => "User email is not in a valid format.";

    private UserEmail _email;

    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    private static readonly Regex EmailRegex = new Regex(EmailPattern, RegexOptions.Compiled);

    internal UserEmailHasToBeValidBusinessRule(UserEmail email)
    {
        _email = email;
    }

    public bool IsBroken()
    {
        return !EmailRegex.IsMatch(_email.Value);
    }
}