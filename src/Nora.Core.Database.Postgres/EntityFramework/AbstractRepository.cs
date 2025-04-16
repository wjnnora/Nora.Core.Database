using Microsoft.EntityFrameworkCore;
using Nora.Core.Domain.Contracts.Repositories;
using Nora.Core.Domain.Entities;

namespace Nora.Core.Database.Postgres.EntityFramework;

public class AbstractRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly DbContext DbContext;

    protected AbstractRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdAsync(TKey id) => await DbSet.FindAsync(id);

    public virtual async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

    public virtual async Task UpdateAsync(TEntity entity)
    {
        var entry = await DbSet.AddAsync(entity);

        entry.State = EntityState.Modified;
    }

    public async Task DeleteByIdAsync(TKey id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity != null)
            DbSet.Remove(entity);
    }
}