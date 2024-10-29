using GameReview.DTOs.User;
using System.Text.Json.Serialization;

namespace GameReview.DTOs.Follow;

public record OutFollowersFollowingDTO(IEnumerable<OutUserDTO> Followers, IEnumerable<OutUserDTO> Following)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<OutUserDTO>? Followers { get; init; } = Followers;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<OutUserDTO>? Following { get; init; } = Following;
}