using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Interfaces;

namespace NotificationService.Hubs;


[Authorize]
internal sealed class NotificationHub : Hub<INotificationClient>
{
    public Task<string> GetConnectionId() => Task.FromResult(Context.ConnectionId);
}