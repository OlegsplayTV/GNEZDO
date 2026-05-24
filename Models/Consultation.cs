namespace GNEZDO.Models;

/// <summary>
/// Запись на консультацию
/// </summary>
public class Consultation
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время консультации
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Длительность (минут)
    /// </summary>
    public int DurationMinutes { get; set; } = 60;

    /// <summary>
    /// Статус консультации
    /// </summary>
    public ConsultationStatus Status { get; set; } = ConsultationStatus.Pending;

    /// <summary>
    /// Комментарий клиента
    /// </summary>
    public string? ClientComment { get; set; }

    /// <summary>
    /// Комментарий специалиста
    /// </summary>
    public string? SpecialistComment { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор специалиста
    /// </summary>
    public int SpecialistId { get; set; }

    // Навигационные свойства
    public virtual User? User { get; set; }
    public virtual Specialist? Specialist { get; set; }
}

/// <summary>
/// Статус консультации
/// </summary>
public enum ConsultationStatus
{
    Pending,      // Ожидает подтверждения
    Confirmed,    // Подтверждена
    Cancelled,    // Отменена
    Completed     // Завершена
}