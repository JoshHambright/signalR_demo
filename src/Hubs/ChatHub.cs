using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspnetCore.SignalR;

namespace signalr.demo.hubs;

public class Chat : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("newMessage", "annonymous", message);
    }
}