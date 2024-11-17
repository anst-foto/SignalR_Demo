using Microsoft.AspNetCore.SignalR.Client;

var hubBuilder = new HubConnectionBuilder();
hubBuilder.WithUrl("https://localhost:7141/chat");
var connection = hubBuilder.Build();
await connection.StartAsync();

connection.On<string>("Receive", Console.WriteLine);
connection.On<long>("Broadcast", tick =>
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(tick);
    Console.ResetColor();
});
connection.Closed += ex =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Connection closed");
    Console.WriteLine(ex?.Message);
    Console.ResetColor();
    
    return Task.CompletedTask;
};

Console.ReadKey();