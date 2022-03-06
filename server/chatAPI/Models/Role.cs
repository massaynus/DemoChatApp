using System.ComponentModel.DataAnnotations;

namespace chatAPI.Models;

public class Role
{
    public short ID { get; set; }

    [StringLength(128)]
    public string RoleName { get; set; }

    public virtual IEnumerable<Account> Accounts { get; set; }
}