using TaskManagerApi.Users;

namespace TaskManagerApi.Auth;

public record UserResponse(long Id, string Email, DateTime CreatedAt)
{
    public static UserResponse From(User user)
    {
        return new UserResponse(user.Id, user.Email, user.CreatedAt);
    }
}