using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;

namespace Application.Common.Interfaces.Data;

public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
        CancellationToken cancellationToken);

    public IExecutionStrategy CreateExecutionStrategy();

    public IDbContextTransaction? GetCurrentTransaction();
}