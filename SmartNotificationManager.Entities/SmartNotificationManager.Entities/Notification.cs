namespace SmartNotificationManager.Entities;

/// <summary>
/// Represents a notification entity
/// </summary>
public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
}

/// <summary>
/// Types of notifications
/// </summary>
public enum NotificationType
{
    Info,
    Warning,
    Error,
    Success
}
