using System.ComponentModel.DataAnnotations;

namespace chatAPI.DTOs;

public class CreateUser
{
    public Guid ID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public DateTime DateOfBirht { get; set; }
}