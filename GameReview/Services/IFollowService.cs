using GameReview.Models;

namespace GameReview.Services;

public interface IFollowService
{
    public Follow FollowUser(string followerId, string followedId);
    public void UnfollowUser(string followerId, string followedId);
}