using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Common.Errors;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.Options;

namespace FidsCodingAssignment.Core.Services;

public class FlightService : ServiceBase, IFlightService
{
    private readonly FlightConfiguration _flightConfiguration;
    
    public FlightService(
        IUnitOfWork uow,
        IOptions<FlightConfiguration> flightConfigurationOptions) : base(uow)
    {
        _flightConfiguration = flightConfigurationOptions.Value;
    }

    public async Task<Result<Flight>> GetFlight(
        string airlineCode,
        int flightNumber,
        DateTime? referenceTime = null)
    {
        var flightEntity = await Uow.Flights.GetFlight(airlineCode, flightNumber);

        if (flightEntity == null)
            return Errors.Flight.NotFound;

        flightEntity.CodeShareFlights = await Uow.Flights.GetCodeShareFlights(flightEntity.Id);
        var flight = Flight.CreateWithStatus(flightEntity, _flightConfiguration, referenceTime);

        return flight;
    }

    public async Task<Result<ICollection<Flight>?>> GetDelayedFlights(TimeSpan delta)
    {
        var activeFlights = (await Uow.Flights.GetActiveFlights())
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
        var flight = await Uow.Flights.GetFlight(airlineCode, flightNumber);

        if (flight == null)
            return Errors.Flight.NotFound;
        
        flight.ActualTime = actualTime;
        
        return Result.Success;
    }
}