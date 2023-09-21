using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Interfaces;

namespace FidsCodingAssignment.Data.Models;

public class AirportEntity : IEntity
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public string Code { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    // TODO: can change city, state, country to use lookup
}