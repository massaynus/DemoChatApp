using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(NormalizedUsername), nameof(AccountID))]
public class User
{
    public Guid ID { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; }

    [StringLength(128)]
    public string LastName { get; set; }

    [StringLength(256)]
    public string Username { get; set; }

    public string NormalizedUsername { get => Username.ToUpperInvariant(); }

    public DateTime DateOfBirht { get; set; }

    public Guid AccountID { get; set; }


    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public DateTime LastStatusChange { get; set; }

    public virtual Status Status { get; set; }
}