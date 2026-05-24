using Microsoft.AspNetCore.Identity;

namespace GNEZDO.Models;

/// <summary>
/// Пользователь платформы (родитель)
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Тип родителя (мама/папа)
    /// </summary>
    public string ParentType { get; set; } = string.Empty;

    /// <summary>
    /// Возраст детей (через запятую)
    /// </summary>
    public string ChildrenAges { get; set; } = string.Empty;

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата последнего входа
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    // Навигационные свойства
    public virtual ICollection<ForumPost>? ForumPosts { get; set; }
    public virtual ICollection<Consultation>? Consultations { get; set; }
}