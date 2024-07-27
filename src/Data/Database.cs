using Dapper;

namespace NotificationService.Data;

public static class Database
{
    public async static Task CreateTablesAsync(DapperDbConnection connection)
    {
        using var db = connection.CreateConnection();

        await db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS "Notifications" (
                "Id" VARCHAR(255) NOT NULL,
                "UserId" VARCHAR(255) NOT NULL,
                "IsRead" BOOLEAN DEFAULT FALSE,
                "Content" VARCHAR(1000) NOT NULL, 
                "Link" VARCHAR(250) NOT NULL, 
                "CreatedAtUtc" TIMESTAMPTZ,
                "UpdatedAtUtc" TIMESTAMPTZ,
                PRIMARY KEY ("Id")
            );
        """);
    }

    public static Task SeedAsync(DapperDbConnection connection)
    {
        throw new NotImplementedException();
    }
}