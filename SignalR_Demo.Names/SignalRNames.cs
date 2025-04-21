namespace SignalR_Demo.Names;

public static class SignalRNames
{
    public static string Receive => nameof(Receive);
    public static string Notify => nameof(Notify);
    public static string Broadcast => nameof(Broadcast);

    public static string ServerSend => "SendAsync";
    public static string ServerBroadcast => "BroadcastAsync";
}