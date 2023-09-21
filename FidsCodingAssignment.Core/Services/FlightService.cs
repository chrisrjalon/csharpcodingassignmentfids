using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Core.Mappers;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Repositories;

namespace FidsCodingAssignment.Core.Services;

public class FlightService : ServiceBase, IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IFlightStatusRepository _flightStatusRepository;
    
    public FlightService(IFlightRepository flightRepository, IFlightStatusRepository flightStatusRepository)
    {
        _flightRepository = flightRepository;
        _flightStatusRepository = flightStatusRepository;
    }

    public async Task<Flight> GetFlight(string airlineCode, int flightNumber)
    {
        var flightEntity = await _flightRepository.GetFlight(airlineCode, flightNumber);

        var flight = flightEntity?.Map();
        
        if (flight == null)
            throw new FidsNotFoundException(nameof(Flight));

        return flight;
    }

    public async Task<FlightStatus> GetFlightStatus(string airlineCode, int flightNumber)
    {
        var flight = await _flightRepository.GetFlight(airlineCode, flightNumber);

        if (flight == null)
            throw new FidsNotFoundException(nameof(Flight));
        
        var currentFlightStatus = await _flightStatusRepository.GetCurrentFlightStatus(flight.Id);

        if (currentFlightStatus == null)
            throw new FidsException($"Flight {flight.FlightNumber} does not have a status at this moment.", ExceptionCategoryType.Info);

        return currentFlightStatus.Map()!;
    }

    public async Task<ICollection<Flight>?> GetDelayedFlights(TimeSpan delta, DateTime? reference = null)
    {
        reference ??= DateTime.UtcNow;
        
        var activeFlights = await _flightRepository.GetActiveFlights();

        var delayedFlights = activeFlights?
            .Where(x => x.ScheduledTime < reference.Value.Add(delta));

        return delayedFlights?.Select(x => x.Map()).ToList();
    }

    public async Task<ICollection<FlightStatus>?> GetFlightStatusHistory(int flightId)
    {
        var flightStatuses = await _flightStatusRepository.GetFlightStatuses(flightId);

        return flightStatuses?.Select(x => x.Map()!).ToList();
    }

    public async Task RecordFlightActualTime(string airlineCode, int flightNumber, DateTime actualTime)
    {
        var flight = await _flightRepository.GetFlight(airlineCode, flightNumber);
        
        if (flight == null)
            throw new FidsNotFoundException(nameof(Flight));
        
        flight.ActualTime = actualTime;
        
        _flightRepository.InsertOrUpdate(flight, 1);
        await _flightRepository.SaveChangesAsync();
    }
}