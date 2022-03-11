namespace chatAPI.DTOs;

public enum UserLoginOperationResult
{
    success,
    failure
}

public class UserLoginResponse
{

    public string Username { get; set; }
    public string JWTToken { get; set; }
    public UserData User { get; set; }
    public UserLoginOperationResult OperationResult { get; set; }
}