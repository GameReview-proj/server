using GameReview.Data;
using GameReview.Data.Builders.Impl;
using GameReview.Models;

namespace GameReview.Services.Impl;

public class FollowService(DatabaseContext context,
    UserService userService,
    FollowBuilder builder) : IFollowService
{
    private readonly DatabaseContext _context = context;
    private readonly FollowBuilder _builder = builder;
    private readonly UserService _userService = userService;

    public Follow FollowUser(string followerId, string followedId)
    {
        var newFollow = _builder
            .SetFollowed(_userService.GetById(followedId))
            .SetFollower(_userService.GetById(followerId))
            .Build();

        _context.Follows.Add(newFollow);
        _context.SaveChanges();

        return newFollow;
    }
}