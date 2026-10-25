using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Auth;

public record RegisterRequest(
    [Required(ErrorMessage = "Email is required")] 
    [EmailAddress(ErrorMessage = "Invalid email format")] 
    [MaxLength(255, ErrorMessage = "Email must be at most 255 characters")]
    string Email,
    
    [Required(ErrorMessage = "Password is required")] 
    [MinLength(12, ErrorMessage = "Password must be at least 12 characters")] 
    [MaxLength(72, ErrorMessage = "Password must be at most 72 characters")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z0-9]).+$", 
        ErrorMessage = "Password must contains uppercase, lowercase, numbers and special characters")]
    string Password);
    