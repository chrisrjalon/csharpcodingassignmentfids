using FidsCodingAssignment.Common.Interfaces;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Contexts;

/// <summary>
/// For example purposes, I've decided to use an in-memory database.
/// </summary>
public class FidsDbContext : DbContext, IContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "FidsDb");
    }

    public DbSet<FlightEntity> Flights { get; set; }
    public DbSet<AirportEntity> Airports { get; set; }
    public DbSet<AirlineEntity> Airlines { get; set; }
    public DbSet<FlightStatusEntity> FlightStatuses { get; set; }
    public DbSet<GateEntity> AirportGates { get; set; }
    public DbSet<GateStatusEntity> AirportGateStatuses { get; set; }

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
}