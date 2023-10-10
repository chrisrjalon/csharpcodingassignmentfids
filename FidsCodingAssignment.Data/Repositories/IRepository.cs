using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
}