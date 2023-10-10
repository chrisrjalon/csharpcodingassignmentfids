using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Common.Errors;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.Options;

namespace FidsCodingAssignment.Core.Services;

public class GateService : ServiceBase, IGateService
{
    private readonly FlightConfiguration _flightConfiguration;
    
    public GateService(
        IUnitOfWork uow, 
        IOptions<FlightConfiguration> flightConfigurationOptions) : base(uow)
    {
        _flightConfiguration = flightConfigurationOptions.Value;
    }
    
    public async Task<Result<Flight?>> GetActiveFlight(string gateCode, DateTime? referenceTime = null)
    {
        if (string.IsNullOrWhiteSpace(gateCode))
            return Errors.Gate.NotFound;

        var flights = (await Uow.Flights.GetFlightsAssignedToGate(gateCode))?
            .Where(x => x.Bound is FlightBoundType.Outbound)?
            .Select(fe => Flight.CreateWithStatus(fe, _flightConfiguration, referenceTime))?
            .Cast<OutboundFlight>()
            .ToList();

        return flights?.FirstOrDefault(x => x.IsAtGate);
    }
}