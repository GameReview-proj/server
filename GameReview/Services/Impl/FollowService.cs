using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Models;

namespace GameReview.Services.Impl;

public class FollowService(DatabaseContext context, UserService userService) : IFollowService
{
    private readonly DatabaseContext _context = context;
    private readonly UserService _userService = userService;

    public Follow FollowUser(string followerId, string followedId)
    {
        var newFollow = FollowAdapter.ToEntity(
            _userService.GetById(followerId),
            _userService.GetById(followedId)
        );

        _context.Follows.Add(newFollow);
        _context.SaveChanges();

        return newFollow;
    }
}