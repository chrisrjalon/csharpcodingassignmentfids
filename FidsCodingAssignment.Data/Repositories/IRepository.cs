﻿using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public interface IRepository<TEntity> : IUnitOfWork where TEntity : IEntity
{
    Task<ICollection<TEntity>?> GetAll();
    
    Task<TEntity?> Get(int id);
}