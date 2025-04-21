using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR_Demo.Names;

namespace SignalR_Demo.Server;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Others.SendAsync(SignalRNames.Notify, $"{Context.ConnectionId} connected");
        await base.OnConnectedAsync();
    }

    public async Task SendAsync(string message)
    {
        await Clients.All.SendAsync(SignalRNames.Receive, message);
    }

    public async Task BroadcastAsync()
    {
        await Clients.All.SendAsync(SignalRNames.Broadcast, DateTime.Now.Ticks);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.Others.SendAsync(SignalRNames.Notify, $"{Context.ConnectionId} disconnected");
        await base.OnDisconnectedAsync(exception);
    }
}