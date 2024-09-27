
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

    public static User Update(InPutUserDTO dto, User user)
    {
        if (!string.IsNullOrEmpty(dto.Email) && !user.Email.Equals(dto.Email)) user.Email = dto.Email;
        if (!string.IsNullOrEmpty(dto.Username) && !user.UserName.Equals(dto.Username)) user.UserName = dto.Username;

        return user;
    }
}