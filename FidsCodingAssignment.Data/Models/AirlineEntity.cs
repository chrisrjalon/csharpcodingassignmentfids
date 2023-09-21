using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class AirlineEntity : IModifiableEntity, IDeletableEntity
{
    /// <summary>
    /// Airline Id.
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
    /// Flag indicating whether the airline is active.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Date deleted.
    /// </summary>
    public DateTime? DateDeleted { get; set; }
    
    /// <summary>
    /// Airline code.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Airline name.
    /// </summary>
    public string Name { get; set; }
}