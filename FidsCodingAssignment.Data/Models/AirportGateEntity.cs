using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class AirportGateEntity : IModifiableEntity, IDeletableEntity
{
    /// <summary>
    /// Gate Id.
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
    /// Flag indicating whether the gate is active.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Date and time when the gate was deleted.
    /// </summary>
    public DateTime? DateDeleted { get; set; }

    /// <summary>
    /// Gate code.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Current gate status.
    /// </summary>
    public GateStatusType? GateStatus { get; set; }
}