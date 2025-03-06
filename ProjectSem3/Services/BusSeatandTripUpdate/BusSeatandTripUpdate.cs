
using Microsoft.EntityFrameworkCore;
using ProjectSem3.Models;

namespace ProjectSem3.Services.BusSeatandTripUpdate;

public class BusSeatandTripUpdate : BackgroundService
{
    private readonly ILogger<BusSeatandTripUpdate> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromHours(1);

    public BusSeatandTripUpdate(ILogger<BusSeatandTripUpdate> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Running scheduled task to update trip and seat statuses...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                await UpdateTripAndSeatsStatus(dbContext);
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    public async Task UpdateTripAndSeatsStatus(DatabaseContext dbContext)
    {
        
        var currentTime = DateTime.Now;

        var tripsToUpdate = dbContext.BusesTrips
            .Where(bt => bt.Trip.DateStart <= currentTime && bt.Trip.Status == 1)
            .ToList();

        var trips = dbContext.Trips.Where(bt => bt.DateStart <= currentTime && bt.Status == 1)
            .ToList();

        foreach (var trip in trips)
        {
            trip.Status = 2;
            
        }

        foreach (var busTrip in tripsToUpdate)
        {
            busTrip.Trip.Status = 2;

            busTrip.Status = 2;
            var bus = dbContext.Buses.SingleOrDefault(b => b.BusId == busTrip.BusId);
            bus.Status = 1;
            var busSeats = dbContext.BusesSeats.Where(bs => bs.BusId == busTrip.BusId).ToList();
            foreach (var seat in busSeats)
            {
                seat.Status = 1;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}
