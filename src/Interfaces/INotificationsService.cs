using NotificationService.Data.Models;
using PR2.Shared.Common;

namespace NotificationService.Interfaces;

public interface INotificationsService
{
    Task<Page<Notification>> GetPageAsync(string userId, PageRequest<Notification> pageRequest, CancellationToken cancellationToken = default);
    Task<Result<Notification>> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default);
    Task<Result<Notification>> ChangeNotificationStateAsync(string id, bool state, CancellationToken cancellationToken = default);
    Task<Result<Notification>> DeleteNotificationAsync(string id, CancellationToken cancellationToken = default);
}