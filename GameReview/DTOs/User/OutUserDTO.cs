namespace GameReview.DTOs.User;

public record OutUserDTO(
        string Id,
        string Username,
        string Email,
        string? ProfilePicture,
        int Followers,
        int Following
    )
{
    public OutUserDTO(Models.User user) : this(user.Id, user.UserName, user.Email, user.Picture, user.Followers.ToList().Count, user.Following.ToList().Count)
    { }
}