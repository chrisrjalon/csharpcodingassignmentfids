using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Repositories;

public interface IRepository<TEntity> : IUnitOfWork where TEntity : class, IEntity
{
    Task<TEntity?> Get(int id);
}