using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(NormalizedUsername))]
public class User
{
    private string username;
    private string normalizedUsername;

    public Guid ID { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; }

    [StringLength(128)]
    public string LastName { get; set; }

    [StringLength(256)]
    public string Username
    {
        get => username;
        set
        {
            username = value;
            normalizedUsername = value.ToUpperInvariant();
        }
    }

    [StringLength(256)]
    public string NormalizedUsername { get => normalizedUsername; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [StringLength(512)]
    public string Password { get; set; }

    public DateTime DateOfBirht { get; set; }

    public DateTime LastStatusChange { get; set; }


    public virtual Role Role { get; set; }

    public virtual Status Status { get; set; }
}