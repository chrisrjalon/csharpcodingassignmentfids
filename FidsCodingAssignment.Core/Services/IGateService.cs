using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Models;

namespace FidsCodingAssignment.Core.Services;

public interface IGateService : IService
{
    Task<Result<Flight?>> GetActiveFlight(string gateCode, DateTime? referenceTime = null);
}