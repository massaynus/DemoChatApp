using System.ComponentModel.DataAnnotations;

namespace chatAPI.DTOs;

public class UserLoginRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}