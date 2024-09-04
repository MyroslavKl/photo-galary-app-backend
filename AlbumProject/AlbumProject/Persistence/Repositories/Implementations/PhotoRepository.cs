using System.Linq.Expressions;
using AlbumProject.Data;
using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts;
using AlbumProject.Persistence.Repositories.Implementations.Common;
using Microsoft.EntityFrameworkCore;

namespace AlbumProject.Persistence.Repositories.Implementations;

public class PhotoRepository:Repository<Photo>,IPhotoRepository
{
    private readonly AlbumDbContext _dbContext;
    public PhotoRepository(AlbumDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Photo>> GetAll(Expression<Func<Photo, bool>>? filter = null)
    {
        return await _dbContext.Photos
            .Where(filter)
            .ToListAsync();
    }
}