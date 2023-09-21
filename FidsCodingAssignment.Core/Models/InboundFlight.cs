using System.Text.Json.Serialization;

namespace FidsCodingAssignment.Core.Models;

public class InboundFlight : Flight
{
    /// <summary>
    /// Flight origin details.
    /// </summary>
    public Location? Origin { get; set; }
}