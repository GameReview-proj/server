using System.ComponentModel.DataAnnotations;

namespace GameReview.Data.DTOs.User;

public record InUserDTO(
    [Required]
    string Username,
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [DataType(DataType.Password)]
    string Password
    )
{ }