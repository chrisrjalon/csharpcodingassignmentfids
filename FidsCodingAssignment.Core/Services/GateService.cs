using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Common.Errors;
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
    
    public async Task<Result<Flight>> GetActiveFlight(string gateCode, DateTime? referenceTime = null)
    {
        if (string.IsNullOrWhiteSpace(gateCode))
            return Errors.Gate.NotFound;
        
        var flights = await _flightRepository.GetFlightsAssignedToGate(gateCode);

        var activeFlight = flights?.FirstOrDefault(x => FlightHelper.IsFlightAtGate(x.ScheduledTime, _flightConfiguration, referenceTime))?.Map();

        if (activeFlight == null)
            return Errors.Gate.NoActiveFlight;
        
        activeFlight.SetStatuses(_flightConfiguration, referenceTime);

        return activeFlight;
    }
}