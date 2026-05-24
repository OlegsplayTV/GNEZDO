using GNEZDO.Models;

namespace GNEZDO.Models;

/// <summary>
/// Категория статей
/// </summary>
public class Category
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание категории
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Slug для URL
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    // Навигационные свойства
    public virtual ICollection<Article>? Articles { get; set; }
}