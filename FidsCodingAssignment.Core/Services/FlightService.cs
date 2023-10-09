using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Extensions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Common.Errors;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.Options;

namespace FidsCodingAssignment.Core.Services;

public class FlightService : ServiceBase, IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly FlightConfiguration _flightConfiguration;
    
    public FlightService(
        IFlightRepository flightRepository,
        IOptions<FlightConfiguration> flightConfigurationOptions)
    {
        _flightRepository = flightRepository;
        _flightConfiguration = flightConfigurationOptions.Value;
    }

    public async Task<Result<Flight>> GetFlight(
        string airlineCode,
        int flightNumber,
        DateTime? referenceTime = null)
    {
        var flightEntity = await _flightRepository.GetFlight(airlineCode, flightNumber);

        if (flightEntity == null)
            return Errors.Flight.NotFound;
        
        var flight = Flight.CreateWithStatus(flightEntity, _flightConfiguration, referenceTime);

        return flight;
    }

    public async Task<Result<ICollection<Flight>?>> GetDelayedFlights(TimeSpan delta, DateTime? referenceTime = null)
    {
        var activeFlights = (await _flightRepository.GetActiveFlights())?
            .Select(fe => Flight.CreateWithStatus(fe, _flightConfiguration, referenceTime))?
            .ToList();

        var delayedFlights = activeFlights?
            .Where(x => x.IsFlightDelayed(delta, referenceTime))?
            .ToList();

        if (delayedFlights.IsNullOrEmpty())
            return delayedFlights;
        
        // in this case we want to override the status of the flight to "Delayed"
        foreach (var flight in delayedFlights!)
            flight.FlightStatus = FlightStatusType.Delayed;
        
        return delayedFlights;
    }

    public async Task<Result> RecordFlightActualTime(string airlineCode, int flightNumber, DateTime actualTime)
    {
        var flight = await _flightRepository.GetFlight(airlineCode, flightNumber);

        if (flight == null)
            return Errors.Flight.NotFound;
        
        flight.ActualTime = actualTime;
        
        await _flightRepository.SaveChangesAsync();
        
        return Result.Success;
    }
}