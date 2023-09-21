namespace FidsCodingAssignment.Core.Models;

public class Airport
{
    /// <summary>
    /// Airport Id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Airport Code.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// City where the airport is located.
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// State where the airport is located.
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// Country where the airport is located.
    /// </summary>
    public string Country { get; set; }
}