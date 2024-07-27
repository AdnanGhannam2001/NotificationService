using NotificationService.Data;
using NotificationService.Interfaces;
using NotificationService.Services;

namespace NotificationService.Extensions;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services.AddScoped<DapperDbConnection>()
            .AddScoped<INotificationsService, NotificationsService>();
    }
}