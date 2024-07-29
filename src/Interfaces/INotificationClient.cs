using NotificationService.Data.Models;

namespace NotificationService.Interfaces;

public interface INotificationClient
{
    Task Notify(Notification notification);
}