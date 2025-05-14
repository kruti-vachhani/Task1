using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.ViewModels;

public class LoginPersonViewModel
{
    [Required(ErrorMessage ="Email is Required.")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9._%+-]*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Please enter a proper email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
     [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
    ErrorMessage = "Password must include uppercase, lowercase, digit, special character, and must not contain spaces.")]
    public string? Password { get; set; }

     public bool RememberMe { get; set; }
}
