using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Интерфейс репозитория статей
/// </summary>
public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAllPublishedAsync();
    Task<Article?> GetByIdAsync(int id);
    Task<IEnumerable<Article>> GetByCategoryAsync(int categoryId);
    Task<Article> CreateAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(int id);
}