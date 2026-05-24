using GNEZDO.Models;

namespace GNEZDO.Services;

/// <summary>
/// Интерфейс сервиса специалистов
/// </summary>
public interface ISpecialistService
{
    Task<IEnumerable<Specialist>> GetAllAvailableAsync();
    Task<Specialist?> GetByIdAsync(int id);
    Task<Consultation> BookConsultationAsync(ConsultationDto dto, string userId);
}