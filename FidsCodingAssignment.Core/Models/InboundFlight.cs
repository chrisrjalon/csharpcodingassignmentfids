using System.Text.Json.Serialization;

namespace FidsCodingAssignment.Core.Models;

public class InboundFlight : Flight
{
    /// <summary>
    /// Flight origin.
    /// </summary>
    public string Origin { get; set; }
}