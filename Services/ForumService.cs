using GNEZDO.Models;
using GNEZDO.Repositories;

namespace GNEZDO.Services;

/// <summary>
/// Сервис для работы с форумом
/// </summary>
public class ForumService : IForumService
{
    private readonly IForumRepository _forumRepository;

    public ForumService(IForumRepository forumRepository)
    {
        _forumRepository = forumRepository;
    }

    public async Task<IEnumerable<ForumTopic>> GetAllTopicsAsync()
    {
        return await _forumRepository.GetAllTopicsAsync();
    }

    public async Task<ForumTopic?> GetTopicByIdAsync(int id)
    {
        return await _forumRepository.GetTopicByIdAsync(id);
    }

    public async Task<IEnumerable<ForumPost>> GetPostsByTopicAsync(int topicId)
    {
        return await _forumRepository.GetPostsByTopicAsync(topicId);
    }

    public async Task<ForumPost> CreatePostAsync(ForumPostDto dto, string userId)
    {
        var post = new ForumPost
        {
            Content = dto.Content,
            ForumTopicId = dto.ForumTopicId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        return await _forumRepository.CreatePostAsync(post);
    }
}