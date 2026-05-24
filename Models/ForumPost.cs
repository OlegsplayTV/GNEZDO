namespace GNEZDO.Models;

/// <summary>
/// Сообщение на форуме
/// </summary>
public class ForumPost
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор темы
    /// </summary>
    public int ForumTopicId { get; set; }

    // Навигационные свойства
    public virtual User? User { get; set; }
    public virtual ForumTopic? ForumTopic { get; set; }
}