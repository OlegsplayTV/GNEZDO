using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Интерфейс репозитория форума
/// </summary>
public interface IForumRepository
{
    Task<IEnumerable<ForumTopic>> GetAllTopicsAsync();
    Task<ForumTopic?> GetTopicByIdAsync(int id);
    Task<IEnumerable<ForumPost>> GetPostsByTopicAsync(int topicId);
    Task<ForumPost> CreatePostAsync(ForumPost post);
    Task<ForumTopic> CreateTopicAsync(ForumTopic topic);
}