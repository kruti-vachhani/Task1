using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.ViewModels;

public class LoginPersonViewModel
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
