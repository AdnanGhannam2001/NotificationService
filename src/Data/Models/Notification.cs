using NanoidDotNet;

namespace NotificationService.Data.Models;

public class Notification(string userId, string content, string link)
{
    public string Id { get; init; } = Nanoid.Generate(size: 15);
    public string UserId { get; init; } = userId;
    public bool IsRead { get; private set; } = false;
    public string Content { get; init; } = content;
    public string Link { get; init; } = link;
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;

    public void Update(bool isRead)
    {
        IsRead = isRead;
    }
}