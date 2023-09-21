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

    public async Task<FlightStatusType> GetFlightStatus(int flightId)
    {
        var flight = await _flightRepository.Get(flightId);
        
        if (flight == null)
            throw new FidsNotFoundException(nameof(Flight));
        
        return flight.FlightStatus;
    }

    public async Task<ICollection<FlightStatus>?> GetFlightStatusHistory(int flightId)
    {
        var flightStatuses = await _flightStatusRepository.GetFlightStatuses(flightId);

        return flightStatuses?.Select(x => x.Map()!).ToList();
    }
}