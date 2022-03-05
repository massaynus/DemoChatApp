namespace chatAPI.Models;

public class Account
{
    public Guid ID { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }

    public Guid UserID { get; set; }
    public virtual Role Role { get; set; }
}