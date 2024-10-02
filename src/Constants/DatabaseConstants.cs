namespace NotificationService.Constants;

internal static class DatabaseConstants
{
#if DOCKER
    public const string ConnectionStringName = "DockerPostgresConnection";
#else
    public const string ConnectionStringName = "PostgresConnection";
#endif // DOCKER 
}