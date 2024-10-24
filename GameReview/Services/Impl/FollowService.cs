using GameReview.Data;
using GameReview.Models;

namespace GameReview.Services.Impl;

public class FollowService(DatabaseContext context, UserService userService) : IFollowService
{
    private readonly DatabaseContext _context = context;
    private readonly UserService _userService = userService;

    public Follow FollowUser(string followerId, string followedId)
    {
        var follower = _userService.GetById(followerId);
        var followed = _userService.GetById(followedId);

        Follow newFollow = new()
        { 
            Followed = followed,
            Follower = follower
        };

        _context.Follows.Add(newFollow);
        _context.SaveChanges();

        return newFollow;
    }
}