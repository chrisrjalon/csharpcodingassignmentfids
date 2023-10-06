using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.TestData;
using FidsCodingAssignment.TestData.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Contexts;

/// <summary>
/// For example purposes, I've decided to use an in-memory database.
/// </summary>
public class FidsDbContext : DbContext, IContext
{
    private readonly TestDataService _testDataService;
    public DbSet<FlightEntity> Flights { get; set; } = null!;
    
    public FidsDbContext(
        DbContextOptions<FidsDbContext> options,
        TestDataService testDataService) : base(options)
    {
        _testDataService = testDataService;
        InitializeTestData();
    }

    public new IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity
    {
        return base.Set<TEntity>();
    }

    public async Task<TEntity?> FindAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        return await base.FindAsync<TEntity>(id);
    }

    public async Task<ICollection<TEntity>?> FindAllAsync<TEntity>() where TEntity : class, IEntity
    {
        return await Set<TEntity>().ToListAsync();
    }

    private void InitializeTestData()
    {
        if (_testDataService.DataInitialized)
            return;
        
        var testData = _testDataService.GetTestData();
        var flights = ConvertToFlightEntities(testData);
        Flights.AddRange(flights);
        SaveChanges();
    }

    private ICollection<FlightEntity> ConvertToFlightEntities(TestDataModel testData)
    {
        return testData.Flights.Select(x =>
            new FlightEntity
            {
                Id = x.FlightId,
                FlightNumber = x.FlightNumber,
                AirlineCode = x.AirlineCode,
                ScheduledTime = x.ScheduleTime!.Value,
                ActualTime = x.ActualTime,
                Bound = x.ArrDep == "DEP" ? FlightBoundType.Outbound : FlightBoundType.Inbound,
                GateCode = x.GateCode,
                FlightType = x.FlightType == "D" ? FlightMovementType.Domestic : FlightMovementType.International,
                ParentFlightId = x.ParentFlightId == 0 ? null : x.ParentFlightId,
                City = x.CityName
            }).ToList();
    }
}