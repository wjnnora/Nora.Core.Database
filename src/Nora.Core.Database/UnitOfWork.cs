using Nora.Core.Database.Contracts.Repositories;
using Nora.Core.Database.Contracts;
using Nora.Core.Database.Delegates;

namespace Nora.Core.Database;

public class UnitOfWork<TContext>(TContext dataBase) : IUnitOfWork<TContext> where TContext : IContext
{
    private readonly TContext _dataBase = dataBase;

    public virtual Task BeginTransactionAsync(TransactionDelegate codeBlock) => _dataBase.BeginTransactionAsync(codeBlock);

    public virtual Task SaveChangesAsync() => _dataBase.SaveChangesAsync();
}