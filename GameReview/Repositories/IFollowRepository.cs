using GameReview.Models;

namespace GameReview.Repositories;

public interface IFollowRepository : IRepository<Follow>
{
    Follow? GetByFollowerIdFollowedId(string followerId, string followedId);
}