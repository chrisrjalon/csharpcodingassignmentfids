using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Repositories;

public interface IRepository<TEntity> : IUnitOfWork where TEntity : class, IEntity
{
    Task<ICollection<TEntity>?> GetAll();
    
    Task<TEntity?> Get(int id);
}