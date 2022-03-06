using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(StatusName))]
public class Status
{
    public short ID { get; set; }

    [StringLength(128)]
    public string StatusName { get; set; }

    public virtual IEnumerable<User> Users { get; set; }
}