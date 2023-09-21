using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Extensions;
using FidsCodingAssignment.Core.Mappers;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;
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

    public async Task SaveFlight(Flight flight)
    {
        var isUpdate = flight.Id != default;
        
        var flightEntity = isUpdate
            ? await _flightRepository.Get(flight.Id)
            : new FlightEntity();
    }

    public async Task<Flight> GetFlight(string airlineCode, int flightNumber)
    {
        var flightEntity = await _flightRepository.GetFlight(airlineCode, flightNumber);

        var flight = flightEntity?.Map();
        
        if (flight == null)
            throw new FidsNotFoundException(nameof(Flight));

        return flight;
    }

    public async Task<FlightStatus> GetFlightStatus(int flightId)
    {
        var flight = await _flightRepository.Get(flightId);
        
        if (flight == null)
            throw new FidsNotFoundException(nameof(Flight));
        
        var currentFlightStatus = await _flightStatusRepository.GetCurrentFlightStatus(flightId);

        if (currentFlightStatus == null)
            throw new FidsException($"Flight {flight.FlightNumber} does not have a status at this moment.", ExceptionCategoryType.Info);

        return currentFlightStatus.Map()!;
    }

    public async Task<ICollection<Flight>> GetDelayedFlights(TimeSpan delta, DateTime? reference = null)
    {
        reference ??= DateTime.UtcNow;
        
        var activeFlights = await _flightRepository.GetActiveFlights();
        
        var delayedOutboundFlights = 
            activeFlights?
                .Where(x => 
                    x.Bound == FlightBoundType.Outbound && 
                    x.ScheduledDeparture < reference.Value.Add(delta))
                .Select(x => x.Map())
                .ToList();
        var delayedInboundFlights =
            activeFlights?
                .Where(x => 
                    x.Bound == FlightBoundType.Inbound &&
                    x.ScheduledArrival < reference.Value.Add(delta))
                .Select(x => x.Map())
                .ToList();

        return delayedOutboundFlights.EmptyIfNull()
            .Concat(delayedInboundFlights.EmptyIfNull())
            .OrderBy(x => x.Id)
            .ToList();
    }

    public async Task<ICollection<FlightStatus>?> GetFlightStatusHistory(int flightId)
    {
        var flightStatuses = await _flightStatusRepository.GetFlightStatuses(flightId);

        return flightStatuses?.Select(x => x.Map()!).ToList();
    }
}