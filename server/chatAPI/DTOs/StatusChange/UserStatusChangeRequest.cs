using System.ComponentModel.DataAnnotations;

namespace chatAPI.DTOs;

public class UserStatusChangeRequest
{
    [Required]
    public string Status { get; set; }
}