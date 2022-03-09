using System.ComponentModel.DataAnnotations;

namespace chatAPI.DTOs;

public class UserSignUpRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; }

    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; }

    public DateTime DateOfBirht { get; set; } = DateTime.UtcNow;
}