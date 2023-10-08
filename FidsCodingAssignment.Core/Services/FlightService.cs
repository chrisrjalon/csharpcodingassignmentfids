using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Extensions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Common.Errors;
using FidsCodingAssignment.Core.Helpers;
using FidsCodingAssignment.Core.Mappers;
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

    public async Task<Result<FlightStatus>> GetFlightStatus(
        string airlineCode,
        int flightNumber,
        DateTime? referenceTime = null)
    {
        var flight = await _flightRepository.GetFlight(airlineCode, flightNumber);

        if (flight == null)
            return Errors.Flight.NotFound;
        
        var status = FlightHelper.GetFlightStatus(flight.Map(), _flightConfiguration, referenceTime);

        return new FlightStatus
        {
            FlightId = flight.Id,
            AirlineCode = flight.AirlineCode,
            FlightNumber = flight.FlightNumber,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            Bound = flight.Bound,
            FlightType = flight.FlightType,
            Status = status
        };
    }

    public async Task<Result<ICollection<Flight>?>> GetDelayedFlights(TimeSpan delta, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;
        
        var activeFlights = await _flightRepository.GetActiveFlights();

        var delayedFlights = activeFlights?
            .Where(x => x.ScheduledTime < referenceTime.Value.Add(delta));

        var result = delayedFlights?.Select(x => x.Map()).ToList();

        if (result.IsNullOrEmpty())
            return result;
        
        // in this case we want to override the status of the flight to "Delayed"
        foreach (var flightResult in result!)
            flightResult.FlightStatus = FlightStatusType.Delayed;
        
        return result;
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