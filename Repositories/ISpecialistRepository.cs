using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Интерфейс репозитория специалистов
/// </summary>
public interface ISpecialistRepository
{
    Task<IEnumerable<Specialist>> GetAllAvailableAsync();
    Task<Specialist?> GetByIdAsync(int id);
    Task<Specialist> CreateAsync(Specialist specialist);
    Task UpdateAsync(Specialist specialist);
}