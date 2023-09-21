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
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualTime { get; set; }
    public DateTime? ScheduledBoardingTime { get; set; }
    public DateTime? ActualBoardingTime { get; set; }
    public int OriginAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public FlightMovementType FlightType { get; set; }
    public FlightStatusType FlightStatus { get; set; }
    public int AirlineId { get; set; }
    public int? GateId { get; set; }
    
    [ForeignKey(nameof(OriginAirportId))]
    public AirportEntity? OriginAirport { get; set; }
    
    [ForeignKey(nameof(DestinationAirportId))]
    public AirportEntity? DestinationAirport { get; set; }
    
    [ForeignKey(nameof(AirlineId))]
    public AirlineEntity? Airline { get; set; }
    
    [ForeignKey(nameof(GateId))]
    public GateEntity? Gate { get; set; }
    
    [ForeignKey(nameof(ParentFlightId))]
    public FlightEntity? ParentFlight { get; set; }
}