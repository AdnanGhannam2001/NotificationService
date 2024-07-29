using NotificationService.Endpoints;
using NotificationService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .RegisterServices();

var app = builder.Build();

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