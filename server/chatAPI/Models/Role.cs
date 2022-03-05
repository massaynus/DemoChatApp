namespace chatAPI.Models;

public class Role
{
    public short ID { get; set; }
    public string RoleName { get; set; }

    public virtual IEnumerable<Account> Accounts { get; set; }
}