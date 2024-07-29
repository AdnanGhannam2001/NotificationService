using MassTransit;
using NotificationService.Data.Models;
using NotificationService.Interfaces;
using PR2.Contracts.Events;

namespace ChatService.Consumers;

// [QueueConsumer]
internal sealed class NotifyEventConsumer : IConsumer<NotifyEvent>
{
    private readonly INotificationsService _service;
    private readonly ILogger<NotifyEventConsumer> _logger;

    public NotifyEventConsumer(INotificationsService service, ILogger<NotifyEventConsumer> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotifyEvent> context)
    {
        var notification = new Notification(context.Message.UserId, context.Message.Content, context.Message.Link);
        await _service.AddNotificationAsync(notification);
    }
}