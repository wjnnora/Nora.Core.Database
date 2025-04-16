using Microsoft.EntityFrameworkCore;
using Nora.Core.Database.Contracts.Repositories;
using Nora.Core.Database.Delegates;

namespace Nora.Core.Database.Postgres.EntityFramework;

public class SqlContext(DbContext dbContext) : ISqlContext
{
    protected readonly DbContext DbContext = dbContext;

    public virtual async Task BeginTransactionAsync(TransactionDelegate codeBlock)
    {
        await using var transaction = await DbContext.Database.BeginTransactionAsync();

        try
        {
            await codeBlock();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();

            throw;
        }
    }

    public virtual async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}