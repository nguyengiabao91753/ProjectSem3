using Microsoft.EntityFrameworkCore;
using ProjectSem3.Models;
using ProjectSem3.Services.BusSeatandTripUpdate;
using Quartz;

namespace ProjectSem3.Jobs;

public class TripSeatStatusJob : IJob
{
    private readonly DatabaseContext _dbContext;
    private readonly BusSeatandTripUpdate busSeatandTripUpdate;

    public TripSeatStatusJob(DatabaseContext dbContext, BusSeatandTripUpdate busSeatandTripUpdate)
    {
        _dbContext = dbContext;
        this.busSeatandTripUpdate = busSeatandTripUpdate;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await busSeatandTripUpdate.UpdateTripAndSeatsStatus(_dbContext);
    }
}
