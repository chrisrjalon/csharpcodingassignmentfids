using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Contexts;

public interface IContext
{
    IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity;

    Task<TEntity?> FindAsync<TEntity>(int id) where TEntity : class, IEntity;
    
    Task<ICollection<TEntity>?> FindAllAsync<TEntity>() where TEntity : class,  IEntity;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}