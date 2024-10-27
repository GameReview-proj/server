using GameReview.Builders;
using GameReview.Models;

namespace GameReview.Builders.Impl;

public class UserBuilder : IUserBuilder
{
    private readonly User _user;

    public UserBuilder()
    {
        _user = new User()
        {
            CreatedDate = DateTime.Now
        };
    }

    public UserBuilder(User user)
    {
        _user = user;
    }

    public IUserBuilder SetEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public IUserBuilder SetUsername(string username)
    {
        _user.UserName = username;
        return this;
    }

    public User Build()
    {
        return _user;
    }
}