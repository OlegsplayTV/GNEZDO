namespace GNEZDO.Models;


/// <summary>
/// Статья с полезной информацией для родителей
/// </summary>
public class Article
{
    /// <summary>
    /// Идентификатор статьи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Заголовок статьи
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Краткое описание
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Полный текст статьи
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Slug для URL
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// URL изображения
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Автор статьи
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Дата публикации
    /// </summary>
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Опубликовано
    /// </summary>
    public bool IsPublished { get; set; } = true;

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int CategoryId { get; set; }

    // Навигационные свойства
    public virtual Category? Category { get; set; }
}