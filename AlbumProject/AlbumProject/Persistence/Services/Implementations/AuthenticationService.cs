using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts;
using AlbumProject.Persistence.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AlbumProject.Persistence.Services.Implementations;
using System.Threading.Tasks;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public AuthenticationService(IUserRepository userRepository,IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<User?> Authenticate(string username,string password)
    {
        var user = await _userRepository.GetOneAsync(obj => obj.Username == username);
        if (user == null)
        {
            throw new Exception("User doesn't exist");
        }
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        
        if (result == PasswordVerificationResult.Success)
        {
            return user; 
        }
        
        return default;
    }

    public async Task RegisterAsync(string username, string password)
    {
        var user = new User
        {
            Username = username,
            Role = "User"
        };
        user.Password = _passwordHasher.HashPassword(user, password);
        await _userRepository.InsertAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}
