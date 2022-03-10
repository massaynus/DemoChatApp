namespace chatAPI.DTOs;

public class UserData
{
    public Guid ID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string Role { get; set; }

    public DateTime DateOfBirht { get; set; }
    public DateTime LastStatusChange { get; set; }
}