using GameReview.Models;

namespace GameReview.Services;

public interface IFollowService
{
    public Follow FollowUser(string followerId, string followedId);
    public void UnfollowUser(string followerId, string followedId);
    public IEnumerable<Follow> GetFollowers(string userId);
    public IEnumerable<Follow> GetFollowings(string userId);
}