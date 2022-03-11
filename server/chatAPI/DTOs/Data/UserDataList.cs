namespace chatAPI.DTOs;

public class UserDataList
{
    public int total { get; set; }
    public int page { get; set; }
    public IEnumerable<UserData> Users { get; set; }
}