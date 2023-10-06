using System.Text.Json.Serialization;

namespace FidsCodingAssignment.TestData.Models;

public class FlightModel
{
    [JsonPropertyName("aircraftregnumber")]
    public string? AircraftRegNumber { get; set; }

    [JsonPropertyName("parentflightid")]
    public int? ParentFlightId { get; set; }

    [JsonPropertyName("remote_airport_sch_dtm")]
    public DateTime? RemoteAirportSchedule { get; set; }

    [JsonPropertyName("remote_airport_act_dtm")]
    public DateTime? RemoteAirportActual { get; set; }

    [JsonPropertyName("airportcode")]
    public string AirportCode { get; set; } = null!;

    [JsonPropertyName("eventtime")]
    public DateTime? EventTime { get; set; }

    [JsonPropertyName("airlinecode")]
    public string AirlineCode { get; set; } = null!;

    [JsonPropertyName("parrentsuffix")]
    public string? ParentSuffix { get; set; }

    public string? Suffix { get; set; }

    [JsonPropertyName("viaairportcodes")]
    public string[]? ViaAirportCodes { get; set; }

    [JsonPropertyName("sched_time")]
    public DateTime? ScheduleTime { get; set; }

    [JsonPropertyName("arrdep")]
    public string ArrDep { get; set; } = null!;

    [JsonPropertyName("bagbelt")]
    public string? BagBelt { get; set; }

    [JsonPropertyName("city_name")]
    public string CityName { get; set; } = null!;

    [JsonPropertyName("flighttype")]
    public string FlightType { get; set; } = null!;

    [JsonPropertyName("remote_airport_est_dtm")]
    public DateTime? RemoteAirportEstimate { get; set; }

    public string? Event { get; set; }

    [JsonPropertyName("aircrafttype")]
    public string AircraftType { get; set; } = null!;

    public string? Tail { get; set; }

    [JsonPropertyName("flightnumber")]
    public int FlightNumber { get; set; }

    [JsonPropertyName("flightid")]
    public int FlightId { get; set; }

    [JsonPropertyName("terminalcode")]
    public string? TerminalCode { get; set; }

    [JsonPropertyName("airline_name")]
    public string AirlineName { get; set; } = null!;

    [JsonPropertyName("actual_time")]
    public DateTime? ActualTime { get; set; }

    [JsonPropertyName("flightstatuscode")]
    public string? FlightStatusCode { get; set; }

    [JsonPropertyName("parentairlinecode")]
    public string? ParentAirlineCode { get; set; }

    [JsonPropertyName("parentfltnumber")]
    public int ParentFlightNumber { get; set; }
    
    [JsonPropertyName("gatecode")]
    public string? GateCode { get; set; }

    public string? Remarks { get; set; }

    [JsonPropertyName("estimated_time")]
    public DateTime EstimatedTime { get; set; }

    [JsonPropertyName("dep_boardingstart_dtm")]
    public DateTime? DepartureBoardingStart { get; set; }   
}