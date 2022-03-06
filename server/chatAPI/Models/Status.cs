using Microsoft.EntityFrameworkCore;

namespace chatAPI.Models;

[Index(nameof(StatusName))]
public class Status
{
    public short ID { get; set; }

    public string StatusName { get; set; }

    public virtual IEnumerable<User> Users { get; set; }
}