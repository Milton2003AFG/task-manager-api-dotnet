using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Auth;

public record LoginRequest(
    [Required, EmailAddress]
    string Email,
    [Required, MinLength(12), MaxLength(72)]
    string Password
    );