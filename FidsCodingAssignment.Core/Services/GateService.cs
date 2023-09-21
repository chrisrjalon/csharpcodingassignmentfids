using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Extensions;
using FidsCodingAssignment.Core.Mappers;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Repositories;

namespace FidsCodingAssignment.Core.Services;

public class GateService : ServiceBase, IGateService
{
    private readonly IGateStatusRepository _gateStatusRepository;
    private readonly IGateRepository _gateRepository;
    
    public GateService(IGateStatusRepository gateStatusRepository, IGateRepository gateRepository)
    {
        _gateStatusRepository = gateStatusRepository;
        _gateRepository = gateRepository;
    }
    
    public async Task<Flight> GetActiveFlight(string gateCode)
    {
        var gate = await _gateRepository.GetByCodeAsync(gateCode);
        
        if (gate == null)
            throw new FidsNotFoundException(nameof(Gate));
        
        var currentGateStatus = await _gateStatusRepository.GetCurrentGateStatus(gate.Id);
        
        if (currentGateStatus == null || currentGateStatus.FlightId.NullOrDefault())
            throw new FidsException($"Gate {gate.Code} is not assigned to a flight at this moment.", ExceptionCategoryType.Info);

        return currentGateStatus.Flight!.Map()!;
    }
}