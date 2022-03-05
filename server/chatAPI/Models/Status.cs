namespace chatAPI.Models;

public class Status
{
    public short ID { get; set; }
    public string StatusName { get; set; }
    public DateTime LastChange { get; set; }

    public virtual IEnumerable<User> Users { get; set; }
}