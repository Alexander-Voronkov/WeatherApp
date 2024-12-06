using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Common.EntitiesAbstractions;

namespace Infrastructure.Data.Converters;
public class GuidTypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
    where TTypedIdValue : BaseEntityId<Guid>
{
    public GuidTypedIdValueConverter(ConverterMappingHints mappingHints = null!)
        : base(id => id.Value, value => Create(value), mappingHints)
    {
    }

    private static TTypedIdValue Create(Guid id) => (Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue)!;
}
