using NotificationService.Constants;
using Npgsql;

namespace NotificationService.Data;

public sealed class DapperDbConnection(IConfiguration configuration)
{
    private readonly string? _connectionString = configuration.GetConnectionString(DatabaseConstants.ConnectionStringName);

    public NpgsqlConnection CreateConnection() => new(_connectionString);
}