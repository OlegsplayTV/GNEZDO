using GNEZDO.Models;

namespace GNEZDO.Services;

/// <summary>
/// Интерфейс сервиса статей
/// </summary>
public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllPublishedAsync();
    Task<Article?> GetByIdAsync(int id);
    Task<IEnumerable<Article>> GetByCategoryAsync(int categoryId);
}