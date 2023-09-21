using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public interface IGateStatusRepository : IRepository<GateStatusEntity>
{
    Task<GateStatusEntity?> GetCurrentGateStatus(int gateId);
}