using GNEZDO.Models;

namespace GNEZDO.Services;

/// <summary>
/// Интерфейс сервиса пользователей
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получить пользователя по ID
    /// </summary>
    Task<User?> GetByIdAsync(string id);

    /// <summary>
    /// Получить пользователя по email
    /// </summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Получить консультации пользователя
    /// </summary>
    Task<IEnumerable<Consultation>> GetConsultationsAsync(string userId);

    /// <summary>
    /// Обновить профиль пользователя
    /// </summary>
    Task UpdateProfileAsync(User user);
}