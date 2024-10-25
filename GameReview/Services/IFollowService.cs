using GameReview.Models;

namespace GameReview.Services;

public interface IFollowService
{
    public Follow FollowUser(string followerId, string followedId);
}