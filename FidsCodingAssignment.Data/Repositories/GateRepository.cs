using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public class GateRepository : RepositoryBase<GateEntity>, IGateRepository
{
    public GateRepository(IContext context) : base(context)
    {
    }
}