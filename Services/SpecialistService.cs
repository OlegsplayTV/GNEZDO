using GNEZDO.Models;
using GNEZDO.Repositories;

namespace GNEZDO.Services;

/// <summary>
/// Сервис для работы со специалистами
/// </summary>
public class SpecialistService : ISpecialistService
{
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IUserRepository _userRepository;

    public SpecialistService(
        ISpecialistRepository specialistRepository,
        IUserRepository userRepository)
    {
        _specialistRepository = specialistRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Specialist>> GetAllAvailableAsync()
    {
        return await _specialistRepository.GetAllAvailableAsync();
    }

    public async Task<Specialist?> GetByIdAsync(int id)
    {
        return await _specialistRepository.GetByIdAsync(id);
    }

    public async Task<Consultation> BookConsultationAsync(ConsultationDto dto, string userId)
    {
        var consultation = new Consultation
        {
            DateTime = dto.DateTime,
            DurationMinutes = dto.DurationMinutes,
            SpecialistId = dto.SpecialistId,
            UserId = userId,
            ClientComment = dto.ClientComment,
            Status = ConsultationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        return await _userRepository.CreateConsultationAsync(consultation);
    }
}