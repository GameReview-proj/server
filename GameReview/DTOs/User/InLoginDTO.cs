using System.ComponentModel.DataAnnotations;

namespace GameReview.DTOs.User;

public record InLoginDTO(
    [Required]
    string Login,
    [Required]
    string Password
    )
{ }