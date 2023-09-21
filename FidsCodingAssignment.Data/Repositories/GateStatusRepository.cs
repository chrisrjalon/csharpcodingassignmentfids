﻿using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Repositories;

public class GateStatusRepository : RepositoryBase<AirportGateStatusEntity>, IGateStatusRepository
{
    public GateStatusRepository(IContext context) : base(context)
    {
    }
    
    public async Task<AirportGateStatusEntity?> GetCurrentGateStatus(int gateId)
    {
        return await Set
            .Include(x => x.Flight)
            .Include(x => x.Gate)
            .FirstOrDefaultAsync(x => x.GateId == gateId && x.IsActive);
    }
}