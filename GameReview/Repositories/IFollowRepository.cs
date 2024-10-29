using GameReview.Models;

namespace GameReview.Repositories;

public interface IFollowRepository : IRepository<Follow>
{
    Follow? GetByFollowerIdFollowedId(string followerId, string followedId);
    IEnumerable<Follow> GetFollowers(string userId);
    IEnumerable<Follow> GetFollowings(string userId);
}