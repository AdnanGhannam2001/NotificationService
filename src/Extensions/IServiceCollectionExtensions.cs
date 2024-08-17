using Microsoft.AspNetCore.DataProtection;
using NotificationService.Data;
using NotificationService.Interfaces;
using NotificationService.Services;

namespace NotificationService.Extensions;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddDataProtection()
            .SetApplicationName("SocialMedia");

        services
            .AddAuthentication("SocialMediaCookies")
            .AddCookie("SocialMediaCookies");

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddRealtimeConnection(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services.AddScoped<DapperDbConnection>()
            .AddScoped<INotificationsService, NotificationsService>();
    }
}