using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.TestData;
using FidsCodingAssignment.TestData.Models;

namespace FidsCodingAssignment.Data.Repositories;

public class TestFlightRepository : RepositoryBase<FlightEntity>, IFlightRepository
{
    private readonly ICollection<FlightEntity> _flights;
    
    public TestFlightRepository(TestDataService testDataService)
    {
        _flights = MapToFlightEntities(testDataService.TestData);
    }
    
    private ICollection<FlightEntity> MapToFlightEntities(TestDataModel testData)
    {
        var testFlights = testData.Flights.Select(x =>
            new FlightEntity
            {
                Id = x.FlightId,
                FlightNumber = x.FlightNumber,
                AirlineCode = x.AirlineCode,
                ScheduledTime = x.ScheduleTime,
                EstimatedTime = x.EstimatedTime,
                ActualTime = x.ActualTime,
                Bound = x.ArrDep == "DEP" ? FlightBoundType.Outbound : FlightBoundType.Inbound,
                GateCode = x.GateCode,
                FlightType = x.FlightType == "D" ? FlightMovementType.Domestic : FlightMovementType.International,
                ParentFlightId = x.ParentFlightId == 0 ? null : x.ParentFlightId,
                City = x.CityName
            }).ToList();

        return testFlights;
    }
    
    public Task<FlightEntity?> GetFlight(string airlineCode, int flightNumber)
    {
        var flight = _flights
            .FirstOrDefault(x => x.AirlineCode == airlineCode && x.FlightNumber == flightNumber);

        return Task.FromResult(flight);
    }

    public Task<ICollection<FlightEntity>?> GetCodeShareFlights(int flightId)
    {
        var flight = _flights
            .FirstOrDefault(x => x.Id == flightId);
        
        if (flight == null)
            return Task.FromResult<ICollection<FlightEntity>?>(null);
        
        var codeShareFlights = _flights
            .Where(x => x.ParentFlightId == flightId)
            .ToList();
        
        codeShareFlights.ForEach(f => f.ParentFlight = flight);

        return Task.FromResult<ICollection<FlightEntity>?>(codeShareFlights);
    }

    public Task<ICollection<FlightEntity>> GetActiveFlights()
    {
        var activeFlights = _flights
            .Where(x => x.ActualTime == null)
            .ToList();
        
        return Task.FromResult<ICollection<FlightEntity>>(activeFlights);
    }

    public Task<ICollection<FlightEntity>> GetFlightsAssignedToGate(string gateCode)
    {
        var flightsAssignedToGate = _flights
            .Where(x => 
                x.Bound == FlightBoundType.Outbound && 
                x.GateCode == gateCode)
            .ToList();
        
        return Task.FromResult<ICollection<FlightEntity>>(flightsAssignedToGate);
    }
}