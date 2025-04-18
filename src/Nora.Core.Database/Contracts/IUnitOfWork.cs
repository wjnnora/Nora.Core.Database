using Nora.Core.Database.Contracts.Repositories;
using Nora.Core.Database.Delegates;

namespace Nora.Core.Database.Contracts;

public interface IUnitOfWork<TContext> where TContext : IContext
{
    Task BeginTransactionAsync(TransactionDelegate codeBlock);

    Task SaveChangesAsync();
}
