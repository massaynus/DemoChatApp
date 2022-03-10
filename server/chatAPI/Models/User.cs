using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(Username), nameof(Email))]
public class User
{
    private string email;
    private DateTime lastStatusChange;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; }

    [StringLength(128)]
    public string LastName { get; set; }

    [Required]
    [Key]
    [StringLength(256)]
    public string Username { get; set; }

    [DataType(DataType.EmailAddress)]
    public virtual string Email { get => email; set => email = value.ToLowerInvariant(); }

    [Required]
    [StringLength(512)]
    public string Password { get; set; }

    public DateTime DateOfBirht { get; set; }

    public virtual DateTime LastStatusChange
    {
        get => lastStatusChange.ToUniversalTime();
        set => lastStatusChange = value.ToUniversalTime();
    }


    public virtual Role Role { get; set; }

    public virtual Status Status { get; set; }
}