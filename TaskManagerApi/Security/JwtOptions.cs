using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Security;

public class JwtOptions
{
    public JwtOptions() { }

    [Required]
    public string Secret { get; init; } = string.Empty;

    [Required]
    public string Issuer { get; init; } = string.Empty;

    [Required]
    public int ExpirationMinutes { get; init; }
}