using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Data.Models;
using NotificationService.Hubs;
using NotificationService.Interfaces;
using PR2.Shared.Common;

namespace NotificationService.Endpoints;

internal static class NotificationsEndpoints
{
    public static RouteGroupBuilder MapNotificationsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetNotificationsPage);
        group.MapPost("/", StartConnection);
        group.MapPatch("{id}", UpdateNotification);
        group.MapDelete("{id}", DeleteNotification);

        return group;
    }

    private static async Task<Ok<Page<Notification>>> GetNotificationsPage(HttpContext context,
        [FromServices] INotificationsService service,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool desc = true)
    {
        _ = context.TryGetUserId(out var userId);
        var pageRequest = new PageRequest<Notification>()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Desc = desc,
        };

        var result = await service.GetPageAsync(userId, pageRequest);
        return TypedResults.Ok(result);
    }

    private static async Task<Ok> StartConnection(HttpContext context,
        [FromServices] IHubContext<NotificationHub, INotificationClient> hub,
        [FromQuery] string connectionId)
    {
        _ = context.TryGetUserId(out var userId);
        await hub.Groups.AddToGroupAsync(connectionId, userId);
        return TypedResults.Ok();
    }

    private static async Task<Results<Ok<Notification>, BadRequest<ExceptionBase[]>>> UpdateNotification(
        HttpContext context,
        [FromServices] INotificationsService service,
        [FromRoute] string id,
        [FromQuery] bool read = true)
    {
        _ = context.TryGetUserId(out var userId);
        var result = await service.ChangeNotificationStateAsync(id, userId, read);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Exceptions);
        }

        return TypedResults.Ok(result.Value);
    }

    private static async Task<Results<Ok<Notification>, BadRequest<ExceptionBase[]>>> DeleteNotification(
        HttpContext context,
        [FromServices] INotificationsService service,
        [FromRoute] string id)
    {
        _ = context.TryGetUserId(out var userId);
        var result = await service.DeleteNotificationAsync(id, userId);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Exceptions);
        }

        return TypedResults.Ok(result.Value);
    }

    public static bool TryGetUserId(this HttpContext context, out string userId)
    {
        var idClaim = context.User.Claims.FirstOrDefault(x => x.Type == "sub");

        if (idClaim is null)
        {
            userId = string.Empty;
            return false;
        }

        userId = idClaim.Value;
        return true;
    }
}