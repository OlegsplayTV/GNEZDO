using GNEZDO.Models;
using GNEZDO.Repositories;

namespace GNEZDO.Services;

/// <summary>
/// Сервис для работы со статьями
/// </summary>
public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IEnumerable<Article>> GetAllPublishedAsync()
    {
        return await _articleRepository.GetAllPublishedAsync();
    }

    public async Task<Article?> GetByIdAsync(int id)
    {
        return await _articleRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Article>> GetByCategoryAsync(int categoryId)
    {
        return await _articleRepository.GetByCategoryAsync(categoryId);
    }
}