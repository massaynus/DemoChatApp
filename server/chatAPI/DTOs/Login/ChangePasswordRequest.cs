using System.ComponentModel.DataAnnotations;

namespace chatAPI.DTOs;

public class ChangePasswordRequest
{
    [Required]
    public Guid ID { get; set; }

    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}