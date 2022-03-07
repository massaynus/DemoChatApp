namespace chatAPI.DTOs;

public class UserLoginResponse
{
    public string Username { get; set; }
    public string JWTToken { get; set; }
}