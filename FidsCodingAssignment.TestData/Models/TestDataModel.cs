using System.Text.Json.Serialization;

namespace FidsCodingAssignment.TestData.Models;

public class TestDataModel
{
    [JsonPropertyName("DFW_GateLounge_Flight_List")]
    public FlightModel[] Flights { get; set; }
}