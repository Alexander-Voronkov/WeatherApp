using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate.ValueObjects;
using Domain.Entities.DomainServices;

namespace Domain.UserAggregate.BusinessRules;
internal class UserHasToBeUniqueBusinessRule : IBusinessRule
{
    public string Message => "User has to be unique.";

    private readonly UserEmail _email;
    private readonly IUserUniquenessHelper _uniquenessHelper;

    internal UserHasToBeUniqueBusinessRule(
        UserEmail email,
        IUserUniquenessHelper uniquenessHelper)
    {
        _email = email;
        _uniquenessHelper = uniquenessHelper;
    }

    public bool IsBroken()
    {
        return !_uniquenessHelper.IsUserUnique(_email).Result;
    }
}