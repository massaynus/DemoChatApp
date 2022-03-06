using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(Username), nameof(AccountID))]
public class User
{
    public Guid ID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirht { get; set; }

    public Guid AccountID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public DateTime LastStatusChange { get; set; }

    public virtual Status Status { get; set; }
}