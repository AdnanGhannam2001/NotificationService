using System.Reflection;
using NotificationService.Endpoints;
using NotificationService.Extensions;
using NotificationService.Hubs;
using PR2.RabbitMQ.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAuth()
    .AddRealtimeConnection()
    .AddCors()
#if DEBUG && !NO_RABBIT_MQ
    .AddRabbitMQ(Assembly.GetExecutingAssembly())
#endif
    .RegisterServices();

var app = builder.Build();
app.HandleCommandArguments(args);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x =>
{
    x
        .SetIsOriginAllowed(x => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/websocket/notification");

app.MapGroup("api/notifications")
    .MapNotificationsEndpoints()
    .RequireAuthorization()
    .WithOpenApi();

app.Run();