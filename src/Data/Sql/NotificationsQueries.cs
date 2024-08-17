namespace NotificationService.Data.Sql;

public static class NotificationsQueries
{
    private const string Table = "\"Notifications\"";
    
    public const string Add = $"""
        INSERT INTO
        {Table} ("Id", "UserId", "Content", "Link", "CreatedAtUtc", "UpdatedAtUtc")
        VALUES  (@Id, @UserId, @Content, @Link, @CreatedAtUtc, @UpdatedAtUtc)
        RETURNING "Id";
    """;

    public const string GetById = $"""
        SELECT *
        FROM {Table}
        WHERE "Id" = @Id AND "UserId" = @UserId;
    """;

    public const string Count = $"""
        SELECT COUNT(*)
        FROM {Table}
        WHERE "UserId" = @UserId;
    """;

    public const string ListAsc = $"""
        SELECT *
        FROM {Table}
        WHERE "UserId" = @UserId
        ORDER BY "UpdatedAtUtc" ASC
        LIMIT @PageSize
        OFFSET @PageNumber * @PageSize;
    """;

    public const string ListDesc = $"""
        SELECT *
        FROM {Table}
        WHERE "UserId" = @UserId
        ORDER BY "UpdatedAtUtc" DESC
        LIMIT @PageSize
        OFFSET @PageNumber * @PageSize;
    """;

    public const string Update = $"""
        UPDATE {Table}
        SET "IsRead" = @IsRead
        WHERE "Id" = @Id;
    """;


    public const string Delete = $"""
        DELETE FROM {Table}
        WHERE "Id" = @Id;
    """;
}