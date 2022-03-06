using System.ComponentModel.DataAnnotations;

namespace chatAPI.Models;

public class Account
{
    public Guid ID { get; set; }

    [StringLength(256)]
    public string Username { get; set; }

    [StringLength(512)]
    public string Password { get; set; }

    public Guid UserID { get; set; }
    public virtual Role Role { get; set; }
}