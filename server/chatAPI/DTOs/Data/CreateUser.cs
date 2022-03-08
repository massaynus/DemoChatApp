namespace chatAPI.DTOs;

public class CreateUser
{
    public Guid ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirht { get; set; }
}