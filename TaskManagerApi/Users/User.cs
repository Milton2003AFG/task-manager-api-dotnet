namespace TaskManagerApi.Users;

public class User
{
    public long Id { get; private set; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}