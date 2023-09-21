using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Repositories;

public class GateRepository : RepositoryBase<GateEntity>, IGateRepository
{
    public GateRepository(IContext context) : base(context)
    {
    }

    public async Task<GateEntity?> GetByCodeAsync(string gateCode)
    {
        return await Set
            .FirstOrDefaultAsync(x => x.Code == gateCode);
    }
}