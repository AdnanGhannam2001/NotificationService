using Dapper;
using NotificationService.Data;
using NotificationService.Data.Models;
using NotificationService.Data.Sql;
using NotificationService.Interfaces;
using Npgsql;
using PR2.Shared.Common;
using PR2.Shared.Exceptions;

namespace NotificationService.Services;

public sealed class NotificationsService : INotificationsService
{
    private readonly NpgsqlConnection _db;
    private readonly ILogger<NotificationsService> _logger;

    public NotificationsService(DapperDbConnection connection, ILogger<NotificationsService> logger)
    {
        _logger = logger;
        _db = connection.CreateConnection();
    }

    public async Task<Page<Notification>> GetPageAsync(string userId, PageRequest<Notification> pageRequest, CancellationToken cancellationToken = default)
    {
        var items = await _db.QueryAsync<Notification>(pageRequest.Desc ? NotificationsQueries.ListDesc : NotificationsQueries.ListAsc,
            new { pageRequest.PageSize, pageRequest.PageNumber, UserId = userId });

        var total = await _db.QueryFirstAsync<int>(NotificationsQueries.Count, new { UserId = userId });

        return new(items, total);
    }

    public async Task<Result<Notification>> GetByIdAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var notification = await _db.QueryFirstOrDefaultAsync<Notification>(
            NotificationsQueries.GetById, new { Id = id, UserId = userId });

        if (notification is null)
        {
            return new RecordNotFoundException($"Notification with Id: {id} is not found");
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return new OperationCancelledException("Operation just got cancelled");
        }

        return notification;
    }

    public async Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await _db.QueryFirstAsync(NotificationsQueries.Add, notification);
    }

    public async Task<Result<Notification>> ChangeNotificationStateAsync(string id, string userId, bool state, CancellationToken cancellationToken = default)
    {
        var notificationResult = await GetByIdAsync(id, userId, cancellationToken);

        if (!notificationResult.IsSuccess)
        {
            return notificationResult;
        }

        notificationResult.Value.Update(state);
        await _db.QueryAsync(NotificationsQueries.Update, notificationResult.Value);

        return notificationResult;
    }

    public async Task<Result<Notification>> DeleteNotificationAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var notificationResult = await GetByIdAsync(id, userId, cancellationToken);

        if (!notificationResult.IsSuccess)
        {
            return notificationResult;
        }

        await _db.QueryAsync(NotificationsQueries.Delete, notificationResult.Value);

        return notificationResult;
    }
}