using System.Text.Json.Serialization;

namespace GameReview.DTOs.User;

public class OutUserDTO
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }

    public OutUserDTO(Models.User user)
    {
        Id = user.Id;
        Username = user.UserName;
        Email = user.Email;
        ProfilePicture = user.Picture;
        Followers = user.Followers?.Count ?? 0;
        Following = user.Following?.Count ?? 0;
    }

    [JsonConstructor]
    public OutUserDTO(string id, string username, string email, string? profilePicture, int followers, int following)
    {
        Id = id;
        Username = username;
        Email = email;
        ProfilePicture = profilePicture;
        Followers = followers;
        Following = following;
    }
}
