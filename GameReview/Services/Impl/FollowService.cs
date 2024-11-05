using GameReview.Builders.Impl;
using GameReview.Infra.RabbitMq;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class FollowService(FollowRepository repository,
    UserService userService,
    FollowBuilder builder,
    RabbitMqProducer rabbitProducer,
    NotificationBuilder notificationBuilder) : IFollowService
{
    private readonly FollowRepository _repository = repository;
    private readonly FollowBuilder _builder = builder;
    private readonly UserService _userService = userService;
    private readonly RabbitMqProducer _rabbitProducer = rabbitProducer;
    private readonly NotificationBuilder _notificationBuilder = notificationBuilder;

    public Follow FollowUser(string followerId, string followedId)
    {
        if (_repository.GetByFollowerIdFollowedId(followerId, followedId) is not null) throw new ConflictException($"Usuário de Id {followerId} já segue usuário de Id {followedId}");

        var newFollow = _builder
            .SetFollowed(_userService.GetById(followedId))
            .SetFollower(_userService.GetById(followerId))
            .Build();

        _repository.Add(newFollow);

        var notification = _notificationBuilder
            .SetUser(newFollow.Followed)
            .SetFollow(newFollow)
            .Build();

        _rabbitProducer.PublishNotification(notification);

        return newFollow;
    }

    public IEnumerable<Follow> GetFollowers(string userId)
    {
        return _repository.GetFollowers(userId);
    }

    public IEnumerable<Follow> GetFollowings(string userId)
    {
        return _repository.GetFollowings(userId);
    }

    public void UnfollowUser(string followerId, string followedId)
    {
        var followFound = _repository.GetByFollowerIdFollowedId(followerId, followedId) ?? throw new NotFoundException($"Usuário de Id {followerId} não segue o usuário de Id {followedId}");

        _repository.Delete(followFound.Id);
    }
}