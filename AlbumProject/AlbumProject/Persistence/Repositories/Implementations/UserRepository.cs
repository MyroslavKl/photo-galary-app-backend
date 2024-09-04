using AlbumProject.Data;
using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts;
using AlbumProject.Persistence.Repositories.Implementations.Common;
using Microsoft.EntityFrameworkCore;

namespace AlbumProject.Persistence.Repositories.Implementations;

public class UserRepository:Repository<User>,IUserRepository
{
    public UserRepository(AlbumDbContext dbContext) : base(dbContext)
    {
    }
    
}