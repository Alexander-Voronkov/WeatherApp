using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Common.EntitiesAbstractions;

namespace Infrastructure.Data.Converters;
public class IntTypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, int>
    where TTypedIdValue : BaseEntityId<int>
{
    public IntTypedIdValueConverter(ConverterMappingHints mappingHints = null!)
        : base(id => id.Value, value => Create(value), mappingHints)
    {
    }

    private static TTypedIdValue Create(int id) => (Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue)!;
}