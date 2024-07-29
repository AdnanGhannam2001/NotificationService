using MassTransit;
using NotificationService.Data.Models;
using NotificationService.Interfaces;
using PR2.Contracts.Events;
using PR2.RabbitMQ.Attributes;

namespace NotificationService.Consumers;

[QueueConsumer]
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

        try
        {
            await _service.AddNotificationAsync(notification);
        }
        catch (Exception)
        {
            _logger.LogCritical(
                "Something went wrong while attempting to create a notficiation for user with id: {UserId} content: {Content} and link: {Link}",
                context.Message.UserId, context.Message.Content, context.Message.Link);
        }
    }
}