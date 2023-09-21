using System.ComponentModel.DataAnnotations.Schema;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class FlightEntity : IModifiableEntity
{
    public int Id { get; set; }
    
    public int CreatedBy { get; set; }
    
    public DateTime DateCreated { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }
    public int FlightNumber { get; set; }   
    public bool IsCodeShare { get; set; }
    public int? ParentFlightId { get; set; }
    public FlightBoundType Bound { get; set; }
    public DateTime ScheduledDeparture { get; set; }
    public DateTime? ActualDeparture { get; set; }
    public DateTime ScheduledArrival { get; set; }
    public DateTime? ActualArrival { get; set; }
    public DateTime? ScheduledBoarding { get; set; }
    public DateTime? ActualBoarding { get; set; }
    public FlightMovementType FlightType { get; set; }
    public FlightStatusType FlightStatus { get; set; }
    public int AirlineId { get; set; }
    public int? GateId { get; set; }
    
    [ForeignKey(nameof(AirlineId))]
    public AirlineEntity? Airline { get; set; }
    
    [ForeignKey(nameof(GateId))]
    public GateEntity? Gate { get; set; }
    
    [ForeignKey(nameof(ParentFlightId))]
    public FlightEntity? ParentFlight { get; set; }
}