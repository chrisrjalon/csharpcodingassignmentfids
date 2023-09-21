using System.ComponentModel;

namespace FidsCodingAssignment.Common.Enumerations;

public enum FlightStatusType
{
    [Description("On Time")]
    OnTime,
    
    [Description("Boarding")]
    Boarding,
    
    [Description("Closed")]
    Closed,
    
    [Description("Delayed")]
    Delayed,
    
    [Description("Departed")]
    Departed,
    
    [Description("En Route")]
    EnRoute,
    
    [Description("Arrived")]
    Arrived,
    
    [Description("Cancelled")]
    Cancelled
}