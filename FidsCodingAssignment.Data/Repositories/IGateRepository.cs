using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public interface IGateRepository : IRepository<GateEntity>
{
    Task<GateEntity?> GetByCodeAsync(string gateCode);
}