using Microsoft.EntityFrameworkCore;
using GNEZDO.Data;
using GNEZDO.Models;

namespace GNEZDO.Repositories;

/// <summary>
/// Репозиторий для работы с форумом
/// </summary>
public class ForumRepository : IForumRepository
{
    private readonly GnezdoContext _context;

    public ForumRepository(GnezdoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ForumTopic>> GetAllTopicsAsync()
    {
        return await _context.ForumTopics
            .Include(t => t.Posts)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<ForumTopic?> GetTopicByIdAsync(int id)
    {
        return await _context.ForumTopics
            .Include(t => t.Posts)
                .ThenInclude(p => (User?)p.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<ForumPost>> GetPostsByTopicAsync(int topicId)
    {
        return await _context.ForumPosts
            .Include(p => p.User)
            .Where(p => p.ForumTopicId == topicId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<ForumPost> CreatePostAsync(ForumPost post)
    {
        _context.ForumPosts.Add(post);

        var topic = await _context.ForumTopics.FindAsync(post.ForumTopicId);
        if (topic != null)
        {
            topic.PostCount++;
        }

        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<ForumTopic> CreateTopicAsync(ForumTopic topic)
    {
        _context.ForumTopics.Add(topic);
        await _context.SaveChangesAsync();
        return topic;
    }
}