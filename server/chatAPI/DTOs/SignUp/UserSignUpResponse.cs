namespace chatAPI.DTOs;

public class UserSignUpResponse
{
    public string Username { get; set; }
    public string Status { get; set; }
    public string Message { get; set; } = "Created";
}