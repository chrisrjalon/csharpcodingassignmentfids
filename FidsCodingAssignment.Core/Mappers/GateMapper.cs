using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class GateMapper
{
    public static Gate Map(this AirportGateEntity gate)
    {
        return new Gate
        {
            Id = gate.Id,
            Code = gate.Code
        };
    }
}