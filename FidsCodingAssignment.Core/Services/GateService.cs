using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Extensions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Extensions;
using FidsCodingAssignment.Core.Helpers;
using FidsCodingAssignment.Core.Mappers;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.Options;

namespace FidsCodingAssignment.Core.Services;

public class GateService : ServiceBase, IGateService
{
    private readonly IFlightRepository _flightRepository;
    private readonly FlightConfiguration _flightConfiguration;
    
    public GateService(
        IFlightRepository flightRepository, 
        IOptions<FlightConfiguration> flightConfigurationOptions)
    {
        _flightRepository = flightRepository;
        _flightConfiguration = flightConfigurationOptions.Value;
    }
    
    public async Task<Flight?> GetActiveFlight(string gateCode, DateTime? referenceTime = null)
    {
        var flights = await _flightRepository.GetFlightsAssignedToGate(gateCode);
        
        if (flights.IsNullOrEmpty())
            throw new FidsException($"No flights assigned to gate {gateCode}");

        var activeFlight = flights.FirstOrDefault(x => FlightHelper.IsFlightAtGate(x.ScheduledTime, _flightConfiguration, referenceTime))?.Map();
        activeFlight?.SetStatuses(_flightConfiguration, referenceTime);

        return activeFlight;
    }
}