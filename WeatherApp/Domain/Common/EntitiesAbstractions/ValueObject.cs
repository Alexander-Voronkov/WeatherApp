using Domain.Common.DomainExceptions;

namespace Domain.Common.EntitiesAbstractions;
public abstract record ValueObject
{
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
