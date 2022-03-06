using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(NormalizedStatusName))]
public class Status
{
    public short ID { get; set; }

    [StringLength(128)]
    public string StatusName { get; set; }

    public string NormalizedStatusName { get => StatusName.ToUpperInvariant(); }

    public virtual IEnumerable<User> Users { get; set; }
}