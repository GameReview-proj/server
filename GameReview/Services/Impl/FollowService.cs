using GameReview.Builders.Impl;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;

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
        if (_repository.GetByFollowerIdFollowedId(followerId, followedId) is not null) throw new ConflictException($"Usuário de Id {followerId} já segue usuário de Id {followedId}");

        var newFollow = _builder
            .SetFollowed(_userService.GetById(followedId))
            .SetFollower(_userService.GetById(followerId))
            .Build();

        _repository.Add(newFollow);

        return newFollow;
    }

    public void UnfollowUser(string followerId, string followedId)
    {
        var followFound = _repository.GetByFollowerIdFollowedId(followerId, followedId) ?? throw new NotFoundException($"Usuário de Id {followerId} não segue o usuário de Id {followedId}");

        _repository.Delete(followFound.Id);
    }
}