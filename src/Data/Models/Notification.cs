using NanoidDotNet;

namespace NotificationService.Data.Models;

public class Notification
{
    #pragma warning disable CS8618
    public Notification() { }
    #pragma warning restore CS8618

    public Notification(string userId, string content, string link)
    {
        Id = Nanoid.Generate(size: 15);
        UserId = userId;
        Content = content;
        IsRead = false;
        Link = link;
        CreatedAtUtc = UpdatedAtUtc = DateTime.UtcNow;
    }

    public string Id { get; init; }
    public string UserId { get; init; }
    public bool IsRead { get; private set; }
    public string Content { get; init; }
    public string Link { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime UpdatedAtUtc { get; private set; }

    public void Update(bool isRead)
    {
        IsRead = isRead;
    }
}