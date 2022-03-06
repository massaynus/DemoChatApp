using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(NormalizedUsername))]
public class User
{
    public Guid ID { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; }

    [StringLength(128)]
    public string LastName { get; set; }

    [StringLength(256)]
    public string Username { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [StringLength(512)]
    public string Password { get; set; }

    public DateTime DateOfBirht { get; set; }

    public DateTime LastStatusChange { get; set; }

    public string NormalizedUsername { get => Username.ToUpperInvariant(); }

    public virtual Role Role { get; set; }

    public virtual Status Status { get; set; }
}