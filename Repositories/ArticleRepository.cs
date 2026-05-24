using Microsoft.EntityFrameworkCore;
using GNEZDO.Data;
using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Репозиторий для работы со статьями
/// </summary>
public class ArticleRepository : IArticleRepository
{
    private readonly GnezdoContext _context;

    public ArticleRepository(GnezdoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAllPublishedAsync()
    {
        return await _context.Articles
            .Include(a => a.Category)
            .Where(a => a.IsPublished)
            .OrderByDescending(a => a.PublishedAt)
            .ToListAsync();
    }

    public async Task<Article?> GetByIdAsync(int id)
    {
        return await _context.Articles
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Article>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Articles
            .Include(a => a.Category)
            .Where(a => a.CategoryId == categoryId && a.IsPublished)
            .OrderByDescending(a => a.PublishedAt)
            .ToListAsync();
    }

    public async Task<Article> CreateAsync(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task UpdateAsync(Article article)
    {
        article.UpdatedAt = DateTime.UtcNow;
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article != null)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}