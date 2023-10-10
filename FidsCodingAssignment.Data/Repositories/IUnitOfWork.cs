namespace FidsCodingAssignment.Data.Repositories;

public interface IUnitOfWork
{
    IFlightRepository Flights { get; }
}