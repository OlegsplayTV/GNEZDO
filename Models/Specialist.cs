using GNEZDO.Models;

namespace GNEZDO.Models;

/// <summary>
/// Специалист (психолог)
/// </summary>
public class Specialist
{
    /// <summary>
    /// Идентификатор специалиста
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Специализация
    /// </summary>
    public string Specialization { get; set; } = string.Empty;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Опыт работы (лет)
    /// </summary>
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Стоимость консультации (руб)
    /// </summary>
    public decimal PricePerHour { get; set; }

    /// <summary>
    /// URL фото
    /// </summary>
    public string? PhotoUrl { get; set; }

    /// <summary>
    /// Доступен для записи
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    // Навигационные свойства
    public virtual ICollection<Consultation>? Consultations { get; set; }
}