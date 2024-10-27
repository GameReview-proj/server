using GameReview.Models;

namespace GameReview.Data.Builders;

public interface IUserBuilder
{
    IUserBuilder SetEmail(string email);
    IUserBuilder SetUsername(string username);
    User Build();
}