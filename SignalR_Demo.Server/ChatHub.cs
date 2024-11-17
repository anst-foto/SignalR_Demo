using Microsoft.AspNetCore.SignalR;

namespace SignalR_Demo.Server;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await this.Clients.Others.SendAsync("Notify", $"{this.Context.ConnectionId} connected");
        await base.OnConnectedAsync();
    }

    public async Task SendAsync(string message)
    {
        await this.Clients.All.SendAsync("Receive", message);
    }
    
    public async Task BroadcastAsync()
    {
        await this.Clients.All.SendAsync("Broadcast", DateTime.Now.Ticks);
    }
}