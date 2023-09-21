using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Extensions;
using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;

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
    
    public async Task<ICollection<TEntity>?> GetAll()
    {
        return await _context.FindAllAsync<TEntity>();
    }

    public async Task<TEntity?> Get(int id)
    {
        return await _context.FindAsync<TEntity>(id);
    }

    public void InsertOrUpdate(TEntity entity, int userId)
    {
        var timeNow = DateTime.UtcNow;
        if (entity is IModifiableEntity modifiableEntity)
        {
            if (modifiableEntity.Id > 0)
            {
                modifiableEntity.ModifiedBy = userId;
                modifiableEntity.LastModified = timeNow;
                return;
            }
        }
        
        entity.CreatedBy = userId;
        entity.DateCreated = timeNow;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}