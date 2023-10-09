using FidsCodingAssignment.Common.Enumerations;
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

    public async Task<Result<ICollection<Flight>?>> GetDelayedFlights(TimeSpan delta)
    {
        var activeFlights = (await _flightRepository.GetActiveFlights())
            .Select(Flight.Create)?
            .ToList();

        var delayedFlights = activeFlights?
            .Where(x => x.IsFlightDelayed(delta))
            .ToList();
        
        delayedFlights?.ForEach(f => f.WithStatus(FlightStatusType.Delayed));
        
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