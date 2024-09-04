using AlbumProject.DTOs;
using AlbumProject.Models;

namespace AlbumProject.Persistence.Services.Contracts;

public interface IAuthenticationService
{
    Task<User?> Authenticate(string username,string password);
    Task RegisterAsync(string username, string password);
    string GenerateJwtToken(User user);
}