 using SignalR_Demo.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
 
app.UseDefaultFiles();
app.UseStaticFiles();
 
app.MapHub<ChatHub>("/chat");
 
app.Run();