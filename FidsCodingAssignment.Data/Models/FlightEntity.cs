﻿using System.ComponentModel.DataAnnotations.Schema;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class FlightEntity : IEntity
{
    public int Id { get; set; }
    public int FlightNumber { get; set; }
    public string AirlineCode { get; set; } = null!;
    public int? ParentFlightId { get; set; }
    public FlightBoundType Bound { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime EstimatedTime { get; set; }
    public DateTime? ActualTime { get; set; }
    public string? City { get; set; }
    public FlightMovementType FlightType { get; set; }
    public string? GateCode { get; set; }
    
    [ForeignKey(nameof(ParentFlightId))]
    public FlightEntity? ParentFlight { get; set; }

    public ICollection<FlightEntity>? CodeShareFlights { get; set; }
}