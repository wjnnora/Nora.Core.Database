using Nora.Core.Database.Delegates;

namespace Nora.Core.Database.Contracts.Repositories;

public interface IContext
{
    Task BeginTransactionAsync(TransactionDelegate codeBlock);

    Task SaveChangesAsync();
}