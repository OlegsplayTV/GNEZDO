using GNEZDO.Models;

namespace GNEZDO.Models;

/// <summary>
/// Тема на форуме
/// </summary>
public class ForumTopic
{
    /// <summary>
    /// Идентификатор темы
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название темы
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание темы
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Количество сообщений
    /// </summary>
    public int PostCount { get; set; }

    // Навигационные свойства
    public virtual ICollection<ForumPost>? Posts { get; set; }
}