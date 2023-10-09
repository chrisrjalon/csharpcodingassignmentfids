using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Contexts;

namespace FidsCodingAssignment.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly IQueryable<TEntity> Set;
    private readonly IContext _context;

    protected RepositoryBase(IContext context)
    {
        _context = context;
        Set = context.Set<TEntity>();
    }

    public async Task<TEntity?> Get(int id)
    {
        return await _context.FindAsync<TEntity>(id);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}