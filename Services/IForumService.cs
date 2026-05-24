using GNEZDO.Models;

namespace GNEZDO.Services;

/// <summary>
/// Интерфейс сервиса форума
/// </summary>
public interface IForumService
{
    Task<IEnumerable<ForumTopic>> GetAllTopicsAsync();
    Task<ForumTopic?> GetTopicByIdAsync(int id);
    Task<IEnumerable<ForumPost>> GetPostsByTopicAsync(int topicId);
    Task<ForumPost> CreatePostAsync(ForumPostDto dto, string userId);
}