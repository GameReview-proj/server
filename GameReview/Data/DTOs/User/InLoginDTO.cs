using System.ComponentModel.DataAnnotations;

namespace GameReview.Data.DTOs.User;

public record InLoginDTO(
    [Required]
    string Login,
    [Required]
    string Password
    )
{ }