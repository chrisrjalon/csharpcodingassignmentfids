using FidsCodingAssignment.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Contexts;

public class FidsDbContext : DbContext, IContext
{
    public FidsDbContext()
    {
        
    }

    public new IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity
    {
        return base.Set<TEntity>();
    }

    public async Task<TEntity?> FindAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        return await base.FindAsync<TEntity>(id);
    }

    public async Task<ICollection<TEntity>?> FindAllAsync<TEntity>() where TEntity : class, IEntity
    {
        return await Set<TEntity>().ToListAsync();
    }
}