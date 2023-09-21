using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public interface IGateStatusRepository : IRepository<AirportGateStatusEntity>
{
    Task<AirportGateStatusEntity?> GetCurrentGateStatus(int gateId);
}