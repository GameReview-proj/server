using GameReview.Models;
using Microsoft.AspNetCore.SignalR;

namespace GameReview.Infra.SignalR;

public class NotificationHub : Hub
{
    public async Task JoinGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }
    
    public async Task LeaveGroup(string userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
    }

    public async Task SendNotification(Notification notification)
    {
        await Clients.All.SendAsync("ReveiceNotification", notification);
    }
}