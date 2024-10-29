using GameReview.Data;
using GameReview.Models;

namespace GameReview.Repositories.Impl;

public class FollowRepository(DatabaseContext context) : Repository<Follow>(context), IFollowRepository
{
    public Follow? GetByFollowerIdFollowedId(string followerId, string followedId)
    {
        return _dbSet
            .FirstOrDefault(f => f.FollowedId == followedId && f.FollowerId == followerId);
    }

    public IEnumerable<Follow> GetFollowers(string userId)
    {
        return _dbSet
            .Where(f => f.FollowedId == userId);
    }

    public IEnumerable<Follow> GetFollowings(string userId)
    {
        return _dbSet
            .Where(f => f.FollowerId == userId);
    }
}