namespace Domain.Common.EntitiesAbstractions;
public interface IBusinessRule
{
    string Message { get; }
    bool IsBroken();
}