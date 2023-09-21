using System.ComponentModel.DataAnnotations.Schema;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class FlightEntity : IModifiableEntity
{
    /// <summary>
    /// Flight Id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Created by user Id.
    /// </summary>
    public int CreatedBy { get; set; }
    
    /// <summary>
    /// Date created.
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    /// Last modified by user Id.
    /// </summary>
    public int? ModifiedBy { get; set; }
    
    /// <summary>
    /// Date last modified.
    /// </summary>
    public DateTime? LastModified { get; set; }
    
    /// <summary>
    /// Flight number.
    /// </summary>
    public int FlightNumber { get; set; }

    /// <summary>
    /// Flag indicating whether this is a CodeShare flight.
    /// </summary>
    public bool IsCodeShare { get; set; }

    /// <summary>
    /// Parent flight Id.
    /// </summary>
    public int? ParentFlightId { get; set; }

    /// <summary>
    /// Parent flight.
    /// </summary>
    [ForeignKey(nameof(ParentFlightId))]
    public FlightEntity? ParentFlight { get; set; }

    /// <summary>
    /// Inbound or outbound flight.
    /// </summary>
    public FlightBoundType Bound { get; set; }
    
    /// <summary>
    /// Scheduled departure time of the flight.
    /// </summary>
    public DateTime ScheduledDeparture { get; set; }

    /// <summary>
    /// Actual departure time of the flight.
    /// </summary>
    public DateTime? ActualDeparture { get; set; }

    /// <summary>
    /// Scheduled arrival time of the flight.
    /// </summary>
    public DateTime ScheduledArrival { get; set; }

    /// <summary>
    /// Actual arrival time of the flight.
    /// </summary>
    public DateTime? ActualArrival { get; set; }

    /// <summary>
    /// Scheduled boarding time of the flight.
    /// </summary>
    public DateTime? ScheduledBoarding { get; set; }

    /// <summary>
    /// Actual boarding time of the flight.
    /// </summary>
    public DateTime? ActualBoarding { get; set; }
    
    /// <summary>
    /// Domestic or international flight.
    /// </summary>
    public FlightMovementType FlightType { get; set; }

    /// <summary>
    /// Current status of the flight.
    /// </summary>
    public FlightStatusType FlightStatus { get; set; }

    /// <summary>
    /// Airline Id of the airline operating the flight.
    /// </summary>
    public int AirlineId { get; set; }

    /// <summary>
    /// Airline operating the flight.
    /// </summary>
    [ForeignKey(nameof(AirlineId))]
    public AirlineEntity? Airline { get; set; }
    
    /// <summary>
    /// Gate Id of the gate used on departure.
    /// </summary>
    public int? GateId { get; set; }

    /// <summary>
    /// Gate used on departure.
    /// </summary>
    [ForeignKey(nameof(GateId))]
    public GateEntity? Gate { get; set; }
}