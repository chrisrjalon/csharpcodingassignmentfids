using System.Text.Json.Serialization;

namespace FidsCodingAssignment.TestData.Models;

public class FlightModel
{
    public string? AircraftRegNumber { get; set; }

    public int ParentFlightId { get; set; }

    [JsonPropertyName("remote_airport_sch_dtm")]
    public DateTime? RemoteAirportSchedule { get; set; }

    [JsonPropertyName("remote_airport_act_dtm")]
    public DateTime? RemoteAirportActual { get; set; }

    public string AirportCode { get; set; }

    public DateTime? EventTime { get; set; }

    public string AirlineCode { get; set; }

    public string ParentSuffix { get; set; }

    public string Suffix { get; set; }

    public ICollection<string>? ViaAirportCodes { get; set; }

    [JsonPropertyName("sched_time")]
    public DateTime? ScheduleTime { get; set; }

    public string ArrDep { get; set; }

    public string? BagBelt { get; set; }

    [JsonPropertyName("city_name")]
    public string CityName { get; set; }

    public string FlightType { get; set; }

    [JsonPropertyName("remote_airport_est_dtm")]
    public DateTime? RemoteAirportEstimate { get; set; }

    public string? Event { get; set; }

    public string AircraftType { get; set; }

    public string? Tail { get; set; }

    public int FlightNumber { get; set; }

    public long FlightId { get; set; }

    public string TerminalCode { get; set; }

    [JsonPropertyName("airline_name")]
    public string AirlineName { get; set; }

    [JsonPropertyName("actual_time")]
    public DateTime? ActualTime { get; set; }

    public string? FlightStatusCode { get; set; }

    public string? ParentAirlineCode { get; set; }

    public int ParentFlightNumber { get; set; }

    public string GateCode { get; set; }

    public string Remarks { get; set; }

    [JsonPropertyName("estimated_time")]
    public DateTime EstimatedTime { get; set; }

    [JsonPropertyName("dep_boardingstart_dtm")]
    public DateTime? DepartureBoardingStart { get; set; }   
}