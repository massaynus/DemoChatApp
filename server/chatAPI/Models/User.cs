using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(Username), nameof(Email))]
public class User
{
    private string email;

    public Guid ID { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; }

    [StringLength(128)]
    public string LastName { get; set; }

    [StringLength(256)]
    public string Username { get; set; }

    [DataType(DataType.EmailAddress)]
    public virtual string Email { get => email; set => email = value.ToLowerInvariant(); }

    [StringLength(512)]
    public string Password { get; set; }

    public DateTime DateOfBirht { get; set; }

    public DateTime LastStatusChange { get; set; }


    public virtual Role Role { get; set; }

    public virtual Status Status { get; set; }
}