using Microsoft.EntityFrameworkCore;
using GNEZDO.Data;
using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly GnezdoContext _context;

    public UserRepository(GnezdoContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdateAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.LastLoginAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
   

    public async Task<IEnumerable<Consultation>> GetUserConsultationsAsync(string userId)
    {
        return await _context.Consultations
            .Include(c => c.Specialist)
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.DateTime)
            .ToListAsync();
    }

    public async Task<Consultation> CreateConsultationAsync(Consultation consultation)
    {
        _context.Consultations.Add(consultation);
        await _context.SaveChangesAsync();
        return consultation;
    }
}