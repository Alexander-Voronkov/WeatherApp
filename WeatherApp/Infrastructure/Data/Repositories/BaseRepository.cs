using Microsoft.EntityFrameworkCore;
using Domain.Common.EntitiesAbstractions;
using Application.Common.Interfaces.Data;
using Infrastructure.Exceptions;

namespace Infrastructure.Data.Repositories;
public class BaseRepository<TEntity, TEntityId, TEntityIdType> : IRepository<TEntity, TEntityId, TEntityIdType>
    where TEntity : BaseEntity<TEntityId, TEntityIdType>
    where TEntityId : BaseEntityId<TEntityIdType>
{
    private readonly DbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public BaseRepository(DbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        var entity = await _entities.FindAsync(new { id.Value }, cancellationToken);
        if (entity == null)
        {
            throw new RepositoryNotFoundException();
        }

        return entity;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var existingEntity = await _entities.FindAsync(new { entity.Id.Value }, cancellationToken);
        if (existingEntity == null)
        {
            throw new RepositoryNotFoundException();
        }

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        var entity = await _entities.FindAsync(new { id.Value }, cancellationToken);
        if (entity == null)
        {
            throw new RepositoryNotFoundException();
        }

        _entities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}