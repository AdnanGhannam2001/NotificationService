using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Data.Models;
using NotificationService.Hubs;
using NotificationService.Interfaces;
using PR2.Contracts.Events;
using PR2.RabbitMQ.Attributes;

namespace NotificationService.Consumers;

[QueueConsumer]
internal sealed class NotifyEventConsumer : IConsumer<NotifyEvent>
{
    private readonly INotificationsService _service;
    private readonly IHubContext<NotificationHub, INotificationClient> _hub;

    private readonly ILogger<NotifyEventConsumer> _logger;

    public NotifyEventConsumer(INotificationsService service,
        IHubContext<NotificationHub, INotificationClient> hub,
        ILogger<NotifyEventConsumer> logger)
    {
        _service = service;
        _hub = hub;

        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotifyEvent> context)
    {
        var notification = new Notification(context.Message.UserId, context.Message.Content, context.Message.Link);

        try
        {
            await _service.AddNotificationAsync(notification);
            await _hub.Clients.Group(notification.UserId).Notify(notification);
        }
        catch (Exception)
        {
            _logger.LogCritical(
                "Something went wrong while attempting to create a notficiation for user with id: {UserId} content: {Content} and link: {Link}",
                context.Message.UserId, context.Message.Content, context.Message.Link);
        }
    }
}