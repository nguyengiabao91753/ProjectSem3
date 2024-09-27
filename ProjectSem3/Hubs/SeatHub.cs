using Microsoft.AspNetCore.SignalR;

namespace ProjectSem3.Hubs;

public class SeatHub : Hub
{
    public async Task SelectSeat(int id, int status)
    {
        await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveSeatSelection", id, status);
    }
}
