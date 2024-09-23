
using GameReview.Data.DTOs.User;
using GameReview.Models;

namespace GameReview.Data.Adapters;

public static class UserAdapter
{
    public static User ToEntity(InUserDTO dto)
    {
        return new()
        {
            Email = dto.Email,
            UserName = dto.Username,
            CreatedDate = DateTime.Now
        };
    }

    public static OutUserDTO ToDTO(User user)
    {
        return new(user.Id,
            user.UserName,
            user.Email
        );
    }
}