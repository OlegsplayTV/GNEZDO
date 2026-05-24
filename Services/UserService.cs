using GNEZDO.Models;
using GNEZDO.Repositories;

namespace GNEZDO.Services;

/// <summary>
/// Сервис для работы с пользователями
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Получить пользователя по email
    /// </summary>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }
    public async Task<IEnumerable<Consultation>> GetConsultationsAsync(string userId)
    {
        return await _userRepository.GetUserConsultationsAsync(userId);
    }

    public async Task UpdateProfileAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }
}