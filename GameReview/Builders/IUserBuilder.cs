using GameReview.Models;

namespace GameReview.Builders;

public interface IUserBuilder
{
    IUserBuilder SetEmail(string email);
    IUserBuilder SetUsername(string username);
    User Build();
}