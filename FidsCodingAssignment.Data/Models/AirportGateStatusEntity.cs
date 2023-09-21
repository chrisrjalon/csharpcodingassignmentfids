using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class AirportGateStatusEntity : IDeletableEntity
{
    /// <summary>
    /// Gate status Id.
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
    /// Flag indicating whether the gate status is active.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Date deleted.
    /// </summary>
    public DateTime? DateDeleted { get; set; }

    /// <summary>
    /// Gate Id for the gate status.
    /// </summary>
    public int GateId { get; set; }

    /// <summary>
    /// Gate for the gate status.
    /// </summary>
    public AirportGateEntity? Gate { get; set; }
    
    /// <summary>
    /// Flight Id of the flight assigned to the gate.
    /// </summary>
    public int? FlightId { get; set; }

    /// <summary>
    /// Flight assigned to the gate.
    /// </summary>
    public FlightEntity? Flight { get; set; }

    /// <summary>
    /// Current gate status.
    /// </summary>
    public GateStatusType GateStatus { get; set; }

    /// <summary>
    /// Remarks for the gate status.
    /// </summary>
    public string? Remarks { get; set; }
}