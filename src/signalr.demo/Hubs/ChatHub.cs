using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace signalr.demo.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class Chat : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("newMessage", Context?.User?.Identity?.Name, message);
    }
}