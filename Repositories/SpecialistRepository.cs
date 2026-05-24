using Microsoft.EntityFrameworkCore;
using GNEZDO.Data;
using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Репозиторий для работы со специалистами
/// </summary>
public class SpecialistRepository : ISpecialistRepository
{
    private readonly GnezdoContext _context;

    public SpecialistRepository(GnezdoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Specialist>> GetAllAvailableAsync()
    {
        return await _context.Specialists
            .Where(s => s.IsAvailable)
            .OrderBy(s => s.LastName)
            .ToListAsync();
    }

    public async Task<Specialist?> GetByIdAsync(int id)
    {
        return await _context.Specialists
            .Include(s => s.Consultations)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Specialist> CreateAsync(Specialist specialist)
    {
        _context.Specialists.Add(specialist);
        await _context.SaveChangesAsync();
        return specialist;
    }

    public async Task UpdateAsync(Specialist specialist)
    {
        _context.Specialists.Update(specialist);
        await _context.SaveChangesAsync();
    }
}