using Microsoft.AspNetCore.SignalR;

namespace NotificationProject.Hubs
{
    public class Notification: Hub
    {
        public async Task SendMessage(string mssg) { 
            await Clients.All.SendAsync("ReceiveMessage",mssg);
        
        }
    }
}
