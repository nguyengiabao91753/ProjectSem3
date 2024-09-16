using Microsoft.AspNetCore.SignalR;

namespace ProjectSem3.Hubs;

public class SeatHub : Hub
{
    public async Task SelectSeat(int id, int status)
    {
        await Clients.All.SendAsync("ReceiveSeatSelection", id, status);
    }
}
