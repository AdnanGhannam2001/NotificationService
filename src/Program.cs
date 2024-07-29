using System.Reflection;
using NotificationService.Endpoints;
using NotificationService.Extensions;
using PR2.RabbitMQ.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
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
app.MapGroup("notifications")
    .MapNotificationsEndpoints()
    .RequireAuthorization();

app.Run();