using FidsCodingAssignment.Core.Models;

namespace FidsCodingAssignment.Core.Services;

public interface IGateService : IService
{
    Task<Flight?> GetActiveFlight(string gateCode, DateTime? referenceTime = null);
}