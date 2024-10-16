using System.ComponentModel.DataAnnotations;

namespace GameReview.Data.DTOs.User;

public record InUserDTO(
    [Required]
    [RegularExpression(@"^\S*$", ErrorMessage = "Não é permitido espaço")]
    string Username,
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [DataType(DataType.Password)]
    string Password
    )
{ }