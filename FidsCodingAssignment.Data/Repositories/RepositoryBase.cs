using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
}