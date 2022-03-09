using System.ComponentModel.DataAnnotations;

namespace chatAPI.Models;

public class Role
{
    public const string DEFAULT_ROLE = "User";

    public short ID { get; set; }

    [StringLength(128)]
    public string RoleName { get; set; }

    public virtual IEnumerable<User> Users { get; set; }
}