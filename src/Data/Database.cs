using DbUp;

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

    public static Task SeedAsync(DapperDbConnection connection)
    {
        throw new NotImplementedException();
    }
}