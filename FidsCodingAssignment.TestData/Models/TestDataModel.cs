using System.Text.Json.Serialization;

namespace FidsCodingAssignment.TestData.Models;

public class TestDataModel
{
    [JsonPropertyName("DFW GateLounge Flight List")]
    public FlightModel[] Flights { get; set; } = null!;
}