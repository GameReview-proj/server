using GameReview.Builders.Impl;
using GameReview.Models;
using GameReview.Repositories.Impl;

namespace GameReview.Services.Impl;

public class FollowService(FollowRepository repository,
    UserService userService,
    FollowBuilder builder) : IFollowService
{
    private readonly FollowRepository _repository = repository;
    private readonly FollowBuilder _builder = builder;
    private readonly UserService _userService = userService;

    public Follow FollowUser(string followerId, string followedId)
    {
        var newFollow = _builder
            .SetFollowed(_userService.GetById(followedId))
            .SetFollower(_userService.GetById(followerId))
            .Build();

        _repository.Add(newFollow);

        return newFollow;
    }
}