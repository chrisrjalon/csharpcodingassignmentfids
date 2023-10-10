
using FidsCodingAssignment.Data.Repositories;

namespace FidsCodingAssignment.Core.Services;

public abstract class ServiceBase : IService
{
    protected readonly IUnitOfWork Uow;

    protected ServiceBase(IUnitOfWork uow)
    {
        Uow = uow;
    }
}