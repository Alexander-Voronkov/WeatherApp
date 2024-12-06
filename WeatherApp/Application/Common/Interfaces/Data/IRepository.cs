using Domain.Common.EntitiesAbstractions;

namespace Application.Common.Interfaces.Data;

public interface IRepository<TEntity, TEntityId, TEntityIdType>
    where TEntity : BaseEntity<TEntityId, TEntityIdType>
    where TEntityId : BaseEntityId<TEntityIdType>
{
    Task<TEntity> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntityId id, CancellationToken cancellationToken = default);
}