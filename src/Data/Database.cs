using Dapper;
using DbUp;
using NotificationService.Data.Sql;

namespace NotificationService.Data;

public static class Database
{
    public static void Init(string connectionString)
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
        
        var upgrader = DeployChanges.To.PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(Database).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            upgrader.PerformUpgrade();
        }
    }

    public static async Task ClearAsync(DapperDbConnection connection)
    {
        using var db = connection.CreateConnection();
        await db.QueryAsync(NotificationsQueries.Clear);
    }

    public static Task SeedAsync(DapperDbConnection connection)
    {
        return Task.CompletedTask;
    }
}