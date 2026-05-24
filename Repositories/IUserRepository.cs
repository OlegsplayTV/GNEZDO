using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Интерфейс репозитория пользователей
/// </summary>
// Repositories/IUserRepository.cs
public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByEmailAsync(string email);  // ← Должен быть!
    Task<User> UpdateAsync(User user);
    Task<IEnumerable<Consultation>> GetUserConsultationsAsync(string userId);
    Task<Consultation> CreateConsultationAsync(Consultation consultation);
}