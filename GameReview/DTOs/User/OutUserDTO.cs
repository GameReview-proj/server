namespace GameReview.DTOs.User;

public record OutUserDTO(
        string Id,
        string Username,
        string Email
    )
{
    public OutUserDTO(Models.User user) : this(user.Id, user.UserName, user.Email)
    { }
}