using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Exceptions;
using TaskManagerApi.Security;
using TaskManagerApi.Users;

namespace TaskManagerApi.Auth;

public class AuthService
{
    private readonly AppDbContext _dbContext;
    private readonly JwtService _jwtService;
    
    public AuthService(AppDbContext dbContext, JwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        string email = request.Email.Trim().ToLowerInvariant();
        bool exists = await _dbContext.Users.AnyAsync(u => u.Email ==  email);
        if (exists)
        {
            throw new UserAlreadyExistsException("Account conflict", "Email already exists");
        }
        
        var user = new User 
            { 
                Email = email, 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), 
                CreatedAt = DateTime.UtcNow 
            };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return UserResponse.From(user);
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        string email = request.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email ==  email);
        if (user is null)
        {
            throw new InvalidCredentialsException("Invalid Credentials", "Incorrect credentials");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException("Invalid Credentials", "Incorrect credentials");
        }
        
        string token = _jwtService.GenerateToken(user);
        return new TokenResponse(token);
    }
}