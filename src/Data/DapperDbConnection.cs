using Npgsql;

namespace NotificationService.Data;

public sealed class DapperDbConnection(IConfiguration configuration)
{
    private static readonly string Name = "PostgresConnection";
    private readonly string? _connectionString = configuration.GetConnectionString(Name);

    public NpgsqlConnection CreateConnection() => new(_connectionString);
}