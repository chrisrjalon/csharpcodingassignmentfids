using System.ComponentModel.DataAnnotations.Schema;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class FlightStatusEntity : IDeletableEntity
{
    /// <summary>
    /// Unique identifier for the flight status.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Unique identifier to identify the creator of the flight status.
    /// </summary>
    public int CreatedBy { get; set; }
    
    /// <summary>
    /// Date and time when the flight status was created.
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    /// Flag indicating whether the flight status is active.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Date and time when the flight status was deleted.
    /// </summary>
    public DateTime? DateDeleted { get; set; }
    
    /// <summary>
    /// The id of the flight.
    /// </summary>
    public int FlightId { get; set; }
    
    /// <summary>
    /// Navigation property for the flight.
    /// </summary>
    public FlightEntity? Flight { get; set; }

    /// <summary>
    /// The status to describe the flight.
    /// </summary>
    public FlightStatusType Status { get; set; }

    /// <summary>
    /// Remarks for the flight status.
    /// </summary>
    public string? Remarks { get; set; }
}