using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Contexts;

public interface IContext
{
    IQueryable<TEntity> Set<TEntity>() where TEntity : IEntity;

    Task<TEntity?> FindAsync<TEntity>(int id) where TEntity : IEntity;
    
    Task<ICollection<TEntity>?> FindAllAsync<TEntity>() where TEntity : IEntity;
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    void SaveChanges();
}